using System.Net;
using System.Net.Http.Json;
using Arzly.Shared.DTOs.Request.Category;
using Arzly.Shared.DTOs.Response.Category;
using Arzly.Tests.Helpers;
using Xunit.Abstractions;

namespace Arzly.IntegrationTests;

public class CategoryIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private readonly HttpClient _adminClient;
    private readonly ITestOutputHelper _output;

    public CategoryIntegrationTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
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
        _output.WriteLine("GET /arzly/v1.0/Category");
        var response = await _client.GetAsync("/arzly/v1.0/Category");
        _output.WriteLine($"Response: {(int)response.StatusCode} {response.StatusCode}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var categories = await response.Content.ReadFromJsonAsync<List<CategoryResponse>>();
        Assert.NotNull(categories);
        _output.WriteLine($"Categories count: {categories.Count}");
        Assert.NotEmpty(categories);
    }

    [Fact]
    public async Task Create_AddsCategory_And_GetById_ReturnsIt()
    {
        var request = new CategoryAddRequest
        {
            Name = "Test Category",
            Description = "A test category description"
        };

        _output.WriteLine($"POST /arzly/v1.0/Category/Create Name={request.Name}");
        var createResponse = await _adminClient.PostAsJsonAsync("/arzly/v1.0/Category/Create", request);
        _output.WriteLine($"Create Response: {(int)createResponse.StatusCode} {createResponse.StatusCode}");
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var created = await createResponse.Content.ReadFromJsonAsync<CategoryResponse>();
        Assert.NotNull(created);
        _output.WriteLine($"Created Id={created.Id}, Name={created.Name}");
        Assert.Equal(request.Name, created.Name);
        Assert.Equal(request.Description, created.Description);
        Assert.NotEqual(Guid.Empty, created.Id);

        _output.WriteLine($"GET /arzly/v1.0/Category/{created.Id}");
        var getResponse = await _client.GetAsync($"/arzly/v1.0/Category/{created.Id}");
        _output.WriteLine($"GetById Response: {(int)getResponse.StatusCode} {getResponse.StatusCode}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var fetched = await getResponse.Content.ReadFromJsonAsync<CategoryResponse>();
        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched.Id);
        Assert.Equal(created.Name, fetched.Name);
        _output.WriteLine("GetById matches created category");
    }

    [Fact]
    public async Task Create_WithoutDescription_Succeeds()
    {
        var request = new CategoryAddRequest { Name = "Minimal Category" };

        _output.WriteLine($"POST /arzly/v1.0/Category/Create Name={request.Name}");
        var response = await _adminClient.PostAsJsonAsync("/arzly/v1.0/Category/Create", request);
        _output.WriteLine($"Response: {(int)response.StatusCode} {response.StatusCode}");
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var created = await response.Content.ReadFromJsonAsync<CategoryResponse>();
        Assert.NotNull(created);
        _output.WriteLine($"Created Id={created.Id}, Name={created.Name}");
        Assert.Equal(request.Name, created.Name);
    }

    [Fact]
    public async Task Update_ModifiesExistingCategory()
    {
        var createRequest = new CategoryAddRequest { Name = "Original Name" };
        var createResponse = await _adminClient.PostAsJsonAsync("/arzly/v1.0/Category/Create", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<CategoryResponse>();
        Assert.NotNull(created);
        _output.WriteLine($"Created category Id={created.Id}");

        var updateRequest = new CategoryUpdateRequest
        {
            Id = created.Id,
            Name = "Updated Name",
            Description = "Updated description"
        };

        _output.WriteLine($"PUT /arzly/v1.0/Category/Update Name={updateRequest.Name}");
        var updateResponse = await _adminClient.PutAsJsonAsync("/arzly/v1.0/Category/Update", updateRequest);
        _output.WriteLine($"Update Response: {(int)updateResponse.StatusCode} {updateResponse.StatusCode}");
        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);

        var updated = await updateResponse.Content.ReadFromJsonAsync<CategoryResponse>();
        Assert.NotNull(updated);
        _output.WriteLine($"Updated Name={updated.Name}, Description={updated.Description}");
        Assert.Equal(updateRequest.Name, updated.Name);
        Assert.Equal(updateRequest.Description, updated.Description);
    }

    [Fact]
    public async Task Delete_RemovesCategory()
    {
        var createRequest = new CategoryAddRequest { Name = "To Delete" };
        var createResponse = await _adminClient.PostAsJsonAsync("/arzly/v1.0/Category/Create", createRequest);
        var created = await createResponse.Content.ReadFromJsonAsync<CategoryResponse>();
        Assert.NotNull(created);
        _output.WriteLine($"Created category Id={created.Id}");

        _output.WriteLine($"DELETE /arzly/v1.0/Category/{created.Id}");
        var deleteResponse = await _adminClient.DeleteAsync($"/arzly/v1.0/Category/{created.Id}");
        _output.WriteLine($"Delete Response: {(int)deleteResponse.StatusCode} {deleteResponse.StatusCode}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task Create_WithImageUrl_Succeeds()
    {
        var request = new CategoryAddRequest
        {
            Name = "Category With Image",
            ImageUrl = "https://example.com/image.jpg"
        };

        _output.WriteLine($"POST /arzly/v1.0/Category/Create Name={request.Name}, ImageUrl={request.ImageUrl}");
        var response = await _adminClient.PostAsJsonAsync("/arzly/v1.0/Category/Create", request);
        _output.WriteLine($"Response: {(int)response.StatusCode} {response.StatusCode}");
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var created = await response.Content.ReadFromJsonAsync<CategoryResponse>();
        Assert.NotNull(created);
        _output.WriteLine($"Created ImageUrl={created.ImageUrl}");
        Assert.Equal(request.ImageUrl, created.ImageUrl);
    }
}
