using Arzly.Client.Services.Contracts;
using Arzly.Client.Services.Contracts.Categories;
using Arzly.Shared.DTOs.Response.Category;

namespace Arzly.Client.Services.ApiClients;

public class CategoryApiClient : BaseApiClient, ICategoryApiClient
{
    public CategoryApiClient(HttpClient httpClient, ILogger<CategoryApiClient> logger) 
        : base(httpClient, logger)
    {
    }

    public async Task<List<CategoryResponse>> GetAllCategoriesAsync()
    {
        var result = await GetAsync<List<CategoryResponse>>("arzly/Category");
        return result ?? new List<CategoryResponse>();
    }

    public async Task<CategoryResponse?> GetCategoryByIdAsync(Guid id)
    {
        return await GetAsync<CategoryResponse>($"arzly/Category/{id}");
    }
}
