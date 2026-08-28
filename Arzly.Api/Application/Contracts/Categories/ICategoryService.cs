using Arzly.Api.Domain.Entities.Listings;
using Arzly.Shared.DTOs.Request.Category;
using Arzly.Shared.DTOs.Response.Category;

namespace Arzly.Api.Application.Contracts.Categories
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse?> GetByIdAsync(Guid id);
        Task<CategoryResponse?> CreateAsync(CategoryAddRequest? request, Guid userId);
        Task<CategoryResponse?> UpdateAsync(CategoryUpdateRequest? request, Guid userId);
        Task<bool> DeleteAsync(Guid id);
    }
}
