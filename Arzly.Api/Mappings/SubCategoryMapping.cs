using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.SubCategory;
using Arzly.Shared.DTOs.Response.SubCategory;

namespace Arzly.Api.Mappings
{
    public static class SubCategoryMapping
    {
        public static SubCategoryResponse ToResponse(this SubCategory entity)
        {
            return new SubCategoryResponse
            {
                Id = entity.Id,
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Description = entity.Description,
                ItemsCount = entity.ItemsCount
            };
        }

        public static SubCategory ToEntity(this SubCategoryAddRequest request)
        {
            return new SubCategory
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Description = request.Description
            };
        }

        public static SubCategory ToEntity(this SubCategoryUpdateRequest request)
        {
            return new SubCategory
            {
                Id = request.Id,
                CategoryId = request.CategoryId,
                Name = request.Name,
                Description = request.Description
            };
        }

        public static SubCategoryUpdateRequest ToUpdateRequest(this SubCategoryResponse response)
        {
            return new SubCategoryUpdateRequest
            {
                Id = response.Id,
                CategoryId = response.CategoryId,
                Name = response.Name,
                Description = response.Description
            };
        }
    }
}
