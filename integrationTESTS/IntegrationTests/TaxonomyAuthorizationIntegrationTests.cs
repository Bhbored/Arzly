using System.Net;
using System.Net.Http.Json;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Shared.DTOs.Request.Category;
using Arzly.Shared.DTOs.Request.SubCategory;
using Arzly.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Arzly.IntegrationTests;

public class TaxonomyAuthorizationIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private readonly HttpClient _adminClient;

    public TaxonomyAuthorizationIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _factory.ResetDatabase();
        _client = factory.CreateClient();
        _adminClient = factory.CreateClient();
        _adminClient.DefaultRequestHeaders.Add(TestAuthHandler.RoleHeader, "admin");
    }

    [Theory]
    [InlineData("user")]
    [InlineData("support")]
    public async Task CategoryMutations_RejectNonAdminRoles(string role)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "/arzly/v1.0/Category/Create")
        {
            Content = JsonContent.Create(new CategoryAddRequest { Name = "Forbidden category" })
        };
        request.Headers.Add(TestAuthHandler.RoleHeader, role);

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task CategoryName_IsUniqueCaseInsensitively()
    {
        var first = await _adminClient.PostAsJsonAsync(
            "/arzly/v1.0/Category/Create",
            new CategoryAddRequest { Name = "Collectibles" });
        var duplicate = await _adminClient.PostAsJsonAsync(
            "/arzly/v1.0/Category/Create",
            new CategoryAddRequest { Name = "  collectibles  " });

        Assert.Equal(HttpStatusCode.Created, first.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, duplicate.StatusCode);
    }

    [Fact]
    public async Task SubcategoryName_IsUniqueWithinParentButAllowedInAnotherCategory()
    {
        var firstCategory = await CreateCategory("First parent");
        var secondCategory = await CreateCategory("Second parent");
        var first = await _adminClient.PostAsJsonAsync(
            "/arzly/v1.0/SubCategory/Create",
            new SubCategoryAddRequest { CategoryId = firstCategory, Name = "Accessories" });
        var duplicate = await _adminClient.PostAsJsonAsync(
            "/arzly/v1.0/SubCategory/Create",
            new SubCategoryAddRequest { CategoryId = firstCategory, Name = "accessories" });
        var otherParent = await _adminClient.PostAsJsonAsync(
            "/arzly/v1.0/SubCategory/Create",
            new SubCategoryAddRequest { CategoryId = secondCategory, Name = "Accessories" });

        Assert.Equal(HttpStatusCode.Created, first.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, duplicate.StatusCode);
        Assert.Equal(HttpStatusCode.Created, otherParent.StatusCode);
    }

    [Fact]
    public async Task CategoryWithSubcategories_CannotBeDeleted()
    {
        var response = await _adminClient.DeleteAsync(
            $"/arzly/v1.0/Category/{TestDataSeeder.VehiclesCategoryId}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Assert.True(await db.Categories.AnyAsync(x => x.Id == TestDataSeeder.VehiclesCategoryId));
    }

    private async Task<Guid> CreateCategory(string name)
    {
        var response = await _adminClient.PostAsJsonAsync(
            "/arzly/v1.0/Category/Create",
            new CategoryAddRequest { Name = name });
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadFromJsonAsync<Arzly.Shared.DTOs.Response.Category.CategoryResponse>();
        return body!.Id;
    }
}
