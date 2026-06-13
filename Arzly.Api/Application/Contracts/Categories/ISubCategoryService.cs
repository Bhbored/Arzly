using Arzly.Api.Domain.Entities.Listings;
using Arzly.Shared.DTOs.Request.SubCategory;
using Arzly.Shared.DTOs.Response.SubCategory;

namespace Arzly.Api.Application.Contracts.Categories
{
    public interface ISubCategoryService : IBaseService<SubCategory, SubCategoryResponse, SubCategoryAddRequest, SubCategoryUpdateRequest, Guid>
    {
        Task<List<SubCategoryResponse>> GetByCategoryIdAsync(Guid categoryId);
        Task<SubCategoryResponse?> GetByTitleAsync(string title);
    }
}
