using System.Net;
using System.Net.Http.Json;
using Arzly.Tests.Helpers;

namespace Arzly.IntegrationTests;

public class ProductionHardeningIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductionHardeningIntegrationTests(CustomWebApplicationFactory factory)
    {
        factory.ResetDatabase();
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData("/health/live")]
    [InlineData("/health/ready")]
    [InlineData("/health/dependencies")]
    public async Task HealthEndpoints_AreAnonymousAndHealthy(string path)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, path);
        request.Headers.Add(TestAuthHandler.AuthenticationHeader, "anonymous");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Requests_EchoAValidCorrelationId()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/health/live");
        request.Headers.Add("X-Correlation-ID", "support-case-123");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("support-case-123", response.Headers.GetValues("X-Correlation-ID").Single());
    }

    [Fact]
    public async Task Requests_ReplaceAnInvalidCorrelationId()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/health/live");
        request.Headers.Add("X-Correlation-ID", new string('x', 129));

        var response = await _client.SendAsync(request);
        var correlationId = response.Headers.GetValues("X-Correlation-ID").Single();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(32, correlationId.Length);
        Assert.True(Guid.TryParseExact(correlationId, "N", out _));
    }

    [Fact]
    public async Task Cors_AllowsConfiguredOriginAndRejectsUnknownOrigin()
    {
        using var allowedRequest = CreatePreflight("https://admin.arzly.test");
        using var rejectedRequest = CreatePreflight("https://malicious.example");

        var allowed = await _client.SendAsync(allowedRequest);
        var rejected = await _client.SendAsync(rejectedRequest);

        Assert.Equal("https://admin.arzly.test",
            allowed.Headers.GetValues("Access-Control-Allow-Origin").Single());
        Assert.False(rejected.Headers.Contains("Access-Control-Allow-Origin"));
    }

    [Fact]
    public async Task AuthenticationEndpoints_ReturnTooManyRequestsAfterConfiguredLimit()
    {
        var responses = new List<HttpResponseMessage>();
        for (var attempt = 0; attempt < 6; attempt++)
        {
            responses.Add(await _client.PostAsJsonAsync(
                "/arzly/v1.0/Authentication/login",
                new { Email = "invalid@arzly.test", Password = "wrong" }));
        }

        Assert.All(responses.Take(5), response =>
            Assert.NotEqual(HttpStatusCode.TooManyRequests, response.StatusCode));
        Assert.Equal(HttpStatusCode.TooManyRequests, responses[5].StatusCode);

        foreach (var response in responses)
            response.Dispose();
    }

    private static HttpRequestMessage CreatePreflight(string origin)
    {
        var request = new HttpRequestMessage(HttpMethod.Options, "/arzly/v1.0/Category");
        request.Headers.Add("Origin", origin);
        request.Headers.Add("Access-Control-Request-Method", "GET");
        return request;
    }
}
