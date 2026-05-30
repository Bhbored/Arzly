using Arzly.Shared.DTOs.Response.Category;

namespace Arzly.Client.Services.Contracts.Categories;

public interface ICategoryApiClient
{
    Task<List<CategoryResponse>> GetAllCategoriesAsync();
    Task<CategoryResponse?> GetCategoryByIdAsync(Guid id);
}
