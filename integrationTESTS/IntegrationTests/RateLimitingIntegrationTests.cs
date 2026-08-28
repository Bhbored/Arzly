using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Arzly.Api.Helpers;
using Arzly.Tests.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Arzly.IntegrationTests;

public class RateLimitingIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly WebApplicationFactory<Program> _limitedFactory;
    private readonly HttpClient _client;

    public RateLimitingIntegrationTests(CustomWebApplicationFactory factory)
    {
        factory.ResetDatabase();
        _factory = factory;
        _limitedFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("RateLimits:EmailDelivery:PermitLimit", "2");
            builder.UseSetting("RateLimits:Credentials:PermitLimit", "2");
            builder.UseSetting("RateLimits:Maps:PermitLimit", "2");
            builder.UseSetting("RateLimits:Support:PermitLimit", "2");
            builder.UseSetting("RateLimits:Writes:PermitLimit", "2");
            builder.UseSetting("RateLimits:Messaging:PermitLimit", "2");
        });
        _client = _limitedFactory.CreateClient();
    }

    [Theory]
    [InlineData("/arzly/v1.0/Email/forgot-password", "POST")]
    [InlineData("/arzly/v1.0/Email/reset-password", "POST")]
    [InlineData("/arzly/v1.0/Location/autocomplete", "GET")]
    [InlineData("/arzly/v1.0/Ticket", "POST")]
    [InlineData("/arzly/v1.0/UserProfile/Update", "PUT")]
    [InlineData("/arzly/v1.0/Chat/SendMessage", "POST")]
    public async Task RiskBasedPolicies_ReturnTooManyRequestsAfterConfiguredLimit(string path, string method)
    {
        var responses = new List<HttpResponseMessage>();
        for (var attempt = 0; attempt < 3; attempt++)
        {
            using var request = new HttpRequestMessage(new HttpMethod(method), path);
            if (method != "GET")
                request.Content = new StringContent("{", Encoding.UTF8, "application/json");
            responses.Add(await _client.SendAsync(request));
        }

        Assert.All(responses.Take(2), response =>
            Assert.NotEqual(HttpStatusCode.TooManyRequests, response.StatusCode));
        Assert.Equal(HttpStatusCode.TooManyRequests, responses[2].StatusCode);
        Assert.True(responses[2].Headers.RetryAfter is not null);

        foreach (var response in responses)
            response.Dispose();
    }

    [Fact]
    public void AuthenticatedPartitions_UseNameIdentifierInsteadOfNameOrIp()
    {
        var first = CreateContext(Guid.NewGuid(), "same-name", IPAddress.Parse("203.0.113.10"));
        var second = CreateContext(Guid.NewGuid(), "same-name", IPAddress.Parse("203.0.113.10"));

        var firstKey = DIContainer.GetRateLimitPartitionKey(first);
        var secondKey = DIContainer.GetRateLimitPartitionKey(second);

        Assert.StartsWith("user:", firstKey);
        Assert.NotEqual(firstKey, secondKey);
    }

    [Fact]
    public void AnonymousPartitions_UseNormalizedRemoteIp()
    {
        var context = new DefaultHttpContext();
        context.Connection.RemoteIpAddress = IPAddress.Parse("203.0.113.10");

        Assert.Equal("ip:::ffff:203.0.113.10", DIContainer.GetRateLimitPartitionKey(context));
    }

    [Fact]
    public void ForwardedHeaders_OnlyAddConfiguredTrustedSources()
    {
        var options = _factory.Services.GetRequiredService<IOptions<ForwardedHeadersOptions>>().Value;

        Assert.Contains(IPAddress.Parse("10.0.0.10"), options.KnownProxies);
        Assert.Contains(System.Net.IPNetwork.Parse("192.0.2.0/24"), options.KnownIPNetworks);
        Assert.Equal(1, options.ForwardLimit);
    }

    [Fact]
    public void EveryControllerMutation_HasAnEffectiveRateLimitPolicy()
    {
        var missing = typeof(Program).Assembly.ExportedTypes
            .Where(type => !type.IsAbstract && typeof(ControllerBase).IsAssignableFrom(type))
            .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly))
            .Where(IsMutationAction)
            .Where(method =>
                method.GetCustomAttribute<EnableRateLimitingAttribute>(inherit: true) is null &&
                method.DeclaringType?.GetCustomAttribute<EnableRateLimitingAttribute>(inherit: true) is null)
            .Select(method => $"{method.DeclaringType!.Name}.{method.Name}")
            .ToList();

        Assert.Empty(missing);
    }

    private static bool IsMutationAction(MethodInfo method) =>
        method.GetCustomAttributes<HttpMethodAttribute>(inherit: true)
            .SelectMany(attribute => attribute.HttpMethods)
            .Any(httpMethod => httpMethod is "POST" or "PUT" or "PATCH" or "DELETE");

    private static DefaultHttpContext CreateContext(Guid userId, string name, IPAddress address)
    {
        var context = new DefaultHttpContext();
        context.Connection.RemoteIpAddress = address;
        context.User = new ClaimsPrincipal(new ClaimsIdentity(
        [
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, name)
        ], "test"));
        return context;
    }
}
