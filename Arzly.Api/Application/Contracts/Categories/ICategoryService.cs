using Arzly.Api.Domain.Entities.Listings;
using Arzly.Shared.DTOs.Request.Category;
using Arzly.Shared.DTOs.Response.Category;

namespace Arzly.Api.Application.Contracts.Categories
{
    public interface ICategoryService : IBaseService<Category, CategoryResponse, CategoryAddRequest, CategoryUpdateRequest, Guid>
    {
    }
}
