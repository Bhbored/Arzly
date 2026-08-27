using System.Net;
using System.Net.Http.Json;
using Arzly.Tests.Helpers;

namespace Arzly.IntegrationTests;

public class AuthorizationIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthorizationIntegrationTests(CustomWebApplicationFactory factory)
    {
        factory.ResetDatabase();
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ProtectedEndpoint_RejectsAnonymousCaller()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/arzly/v1.0/Category");
        request.Headers.Add(TestAuthHandler.AuthenticationHeader, "anonymous");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task AnonymousEndpoint_AllowsAnonymousCaller()
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "/arzly/v1.0/Authentication/login")
        {
            Content = JsonContent.Create(new { })
        };
        request.Headers.Add(TestAuthHandler.AuthenticationHeader, "anonymous");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ProtectedEndpoint_AllowsAuthenticatedUser()
    {
        var response = await _client.GetAsync("/arzly/v1.0/Category");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("user")]
    [InlineData("support")]
    public async Task AdminEndpoint_RejectsNonAdminRole(string role)
    {
        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            "/arzly/v1.0/admin/ListingAdmin/get-all");
        request.Headers.Add(TestAuthHandler.RoleHeader, role);

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task AdminEndpoint_AllowsAdminRole()
    {
        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            "/arzly/v1.0/admin/ListingAdmin/get-all");
        request.Headers.Add(TestAuthHandler.RoleHeader, "admin");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
