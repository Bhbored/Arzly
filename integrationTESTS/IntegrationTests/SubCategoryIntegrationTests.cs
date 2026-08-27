using System.Net;
using System.Net.Http.Json;
using Arzly.Shared.DTOs.Request.SubCategory;
using Arzly.Shared.DTOs.Response.SubCategory;
using Arzly.Tests.Helpers;
using Xunit.Abstractions;

namespace Arzly.IntegrationTests;

public class SubCategoryIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private static readonly Guid VehiclesCategoryId = Guid.Parse("A1B2C3D4-0001-0001-0001-000000000001");

    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private readonly HttpClient _adminClient;
    private readonly ITestOutputHelper _output;

    public SubCategoryIntegrationTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;
        _factory.ResetDatabase();
        _client = factory.CreateClient();
        _adminClient = factory.CreateClient();
        _adminClient.DefaultRequestHeaders.Add(TestAuthHandler.RoleHeader, "admin");
    }

    [Fact]
    public async Task GetAll_ReturnsSeedData()
    {
        _output.WriteLine("GET /arzly/v1.0/SubCategory");
        var response = await _client.GetAsync("/arzly/v1.0/SubCategory");
        _output.WriteLine($"Response: {(int)response.StatusCode} {response.StatusCode}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var subCategories = await response.Content.ReadFromJsonAsync<List<SubCategoryResponse>>();
        Assert.NotNull(subCategories);
        _output.WriteLine($"SubCategories count: {subCategories.Count}");
        Assert.NotEmpty(subCategories);
    }

    [Fact]
    public async Task GetByCategoryId_ReturnsSubCategories()
    {
        _output.WriteLine($"GET /arzly/v1.0/SubCategory/category/{VehiclesCategoryId}");
        var response = await _client.GetAsync($"/arzly/v1.0/SubCategory/category/{VehiclesCategoryId}");
        _output.WriteLine($"Response: {(int)response.StatusCode} {response.StatusCode}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var subCategories = await response.Content.ReadFromJsonAsync<List<SubCategoryResponse>>();
        Assert.NotNull(subCategories);
        _output.WriteLine($"SubCategories for Vehicles: {subCategories.Count}");
        Assert.NotEmpty(subCategories);
        Assert.All(subCategories, sc => Assert.Equal(VehiclesCategoryId, sc.CategoryId));
    }

    [Fact]
    public async Task GetByTitle_ReturnsSubCategory()
    {
        _output.WriteLine("GET /arzly/v1.0/SubCategory/by-title/Cars For Sale");
        var response = await _client.GetAsync("/arzly/v1.0/SubCategory/by-title/Cars For Sale");
        _output.WriteLine($"Response: {(int)response.StatusCode} {response.StatusCode}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var subCategory = await response.Content.ReadFromJsonAsync<SubCategoryResponse>();
        Assert.NotNull(subCategory);
        _output.WriteLine($"Found: Id={subCategory.Id}, Name={subCategory.Name}");
        Assert.Equal("Cars For Sale", subCategory.Name);
        Assert.Equal(VehiclesCategoryId, subCategory.CategoryId);
    }

    [Fact]
    public async Task GetByTitle_ReturnsNotFound_ForUnknownTitle()
    {
        _output.WriteLine("GET /arzly/v1.0/SubCategory/by-title/NonExistentSubCategory");
        var response = await _client.GetAsync("/arzly/v1.0/SubCategory/by-title/NonExistentSubCategory");
        _output.WriteLine($"Response: {(int)response.StatusCode} {response.StatusCode}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetById_WithSeedId_ReturnsSubCategory()
    {
        var seedId = Guid.Parse("B1B2C3D4-0002-0002-0002-000000000001");
        _output.WriteLine($"GET /arzly/v1.0/SubCategory/{seedId}");
        var response = await _client.GetAsync($"/arzly/v1.0/SubCategory/{seedId}");
        _output.WriteLine($"Response: {(int)response.StatusCode} {response.StatusCode}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var subCategory = await response.Content.ReadFromJsonAsync<SubCategoryResponse>();
        Assert.NotNull(subCategory);
        _output.WriteLine($"Found: Id={subCategory.Id}, Name={subCategory.Name}");
        Assert.Equal(seedId, subCategory.Id);
    }

    [Fact]
    public async Task Create_AddsSubCategory_And_GetById_ReturnsIt()
    {
        var request = new SubCategoryAddRequest
        {
            CategoryId = VehiclesCategoryId,
            Name = "Test SubCategory",
            Description = "A test subcategory"
        };

        _output.WriteLine($"POST /arzly/v1.0/SubCategory/Create Name={request.Name}, CategoryId={request.CategoryId}");
        var createResponse = await _adminClient.PostAsJsonAsync("/arzly/v1.0/SubCategory/Create", request);
        _output.WriteLine($"Create Response: {(int)createResponse.StatusCode} {createResponse.StatusCode}");
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var created = await createResponse.Content.ReadFromJsonAsync<SubCategoryResponse>();
        Assert.NotNull(created);
        _output.WriteLine($"Created Id={created.Id}, Name={created.Name}, CategoryId={created.CategoryId}");
        Assert.Equal(request.Name, created.Name);
        Assert.Equal(request.CategoryId, created.CategoryId);
        Assert.NotEqual(Guid.Empty, created.Id);

        _output.WriteLine($"GET /arzly/v1.0/SubCategory/{created.Id}");
        var getResponse = await _client.GetAsync($"/arzly/v1.0/SubCategory/{created.Id}");
        _output.WriteLine($"GetById Response: {(int)getResponse.StatusCode} {getResponse.StatusCode}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var fetched = await getResponse.Content.ReadFromJsonAsync<SubCategoryResponse>();
        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched.Id);
        Assert.Equal(created.Name, fetched.Name);
        _output.WriteLine("GetById matches created subcategory");
    }

    [Fact]
    public async Task Create_WithoutDescription_Succeeds()
    {
        var request = new SubCategoryAddRequest
        {
            CategoryId = VehiclesCategoryId,
            Name = "Minimal SubCategory"
        };

        _output.WriteLine($"POST /arzly/v1.0/SubCategory/Create Name={request.Name}");
        var response = await _adminClient.PostAsJsonAsync("/arzly/v1.0/SubCategory/Create", request);
        _output.WriteLine($"Response: {(int)response.StatusCode} {response.StatusCode}");
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var created = await response.Content.ReadFromJsonAsync<SubCategoryResponse>();
        Assert.NotNull(created);
        _output.WriteLine($"Created Id={created.Id}, Name={created.Name}");
        Assert.Equal(request.Name, created.Name);
    }

    [Fact]
    public async Task Update_ModifiesExistingSubCategory()
    {
        var createRequest = new SubCategoryAddRequest
        {
            CategoryId = VehiclesCategoryId,
            Name = "Original Name"
        };
        var createResponse = await _adminClient.PostAsJsonAsync("/arzly/v1.0/SubCategory/Create", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<SubCategoryResponse>();
        Assert.NotNull(created);
        _output.WriteLine($"Created subcategory Id={created.Id}");

        var updateRequest = new SubCategoryUpdateRequest
        {
            Id = created.Id,
            CategoryId = VehiclesCategoryId,
            Name = "Updated Name",
            Description = "Updated description"
        };

        _output.WriteLine($"PUT /arzly/v1.0/SubCategory/Update Name={updateRequest.Name}");
        var updateResponse = await _adminClient.PutAsJsonAsync("/arzly/v1.0/SubCategory/Update", updateRequest);
        _output.WriteLine($"Update Response: {(int)updateResponse.StatusCode} {updateResponse.StatusCode}");
        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);

        var updated = await updateResponse.Content.ReadFromJsonAsync<SubCategoryResponse>();
        Assert.NotNull(updated);
        _output.WriteLine($"Updated Name={updated.Name}, Description={updated.Description}");
        Assert.Equal(updateRequest.Name, updated.Name);
        Assert.Equal(updateRequest.Description, updated.Description);
    }

    [Fact]
    public async Task Delete_RemovesSubCategory()
    {
        var createRequest = new SubCategoryAddRequest
        {
            CategoryId = VehiclesCategoryId,
            Name = "To Delete"
        };
        var createResponse = await _adminClient.PostAsJsonAsync("/arzly/v1.0/SubCategory/Create", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<SubCategoryResponse>();
        Assert.NotNull(created);
        _output.WriteLine($"Created subcategory Id={created.Id}");

        _output.WriteLine($"DELETE /arzly/v1.0/SubCategory/{created.Id}");
        var deleteResponse = await _adminClient.DeleteAsync($"/arzly/v1.0/SubCategory/{created.Id}");
        _output.WriteLine($"Delete Response: {(int)deleteResponse.StatusCode} {deleteResponse.StatusCode}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }
}
