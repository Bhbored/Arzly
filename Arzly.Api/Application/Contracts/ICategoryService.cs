using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.Category;
using Arzly.Shared.DTOs.Response.Category;

namespace Arzly.Api.Application.Contracts
{
    public interface ICategoryService : IBaseService<Category, CategoryResponse, CategoryAddRequest, CategoryUpdateRequest, Guid>
    {
    }
}
