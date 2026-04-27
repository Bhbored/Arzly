using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.SubCategory;
using Arzly.Shared.DTOs.Response.SubCategory;

namespace Arzly.Api.Application.Contracts
{
    public interface ISubCategoryService : IBaseService<SubCategory, SubCategoryResponse, SubCategoryAddRequest, SubCategoryUpdateRequest, Guid>
    {
    }
}
