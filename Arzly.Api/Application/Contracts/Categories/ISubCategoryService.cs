using Arzly.Api.Domain.Entities.Listings;
using Arzly.Shared.DTOs.Request.SubCategory;
using Arzly.Shared.DTOs.Response.SubCategory;

namespace Arzly.Api.Application.Contracts.Categories
{
    public interface ISubCategoryService
    {
        Task<List<SubCategoryResponse>> GetAllAsync();
        Task<SubCategoryResponse?> GetByIdAsync(Guid id);
        Task<SubCategoryResponse?> CreateAsync(SubCategoryAddRequest? request, Guid userId);
        Task<SubCategoryResponse?> UpdateAsync(SubCategoryUpdateRequest? request, Guid userId);
        Task<bool> DeleteAsync(Guid id);
        Task<List<SubCategoryResponse>> GetByCategoryIdAsync(Guid categoryId);
        Task<SubCategoryResponse?> GetByTitleAsync(string title);
    }
}
