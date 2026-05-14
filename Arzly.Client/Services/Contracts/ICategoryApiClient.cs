using Arzly.Shared.DTOs.Response.Category;

namespace Arzly.Client.Services.Contracts;

public interface ICategoryApiClient
{
    Task<List<CategoryResponse>> GetAllCategoriesAsync();
    Task<CategoryResponse?> GetCategoryByIdAsync(Guid id);
}
