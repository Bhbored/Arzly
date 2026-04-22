using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.Category;
using Arzly.Shared.DTOs.Response.Category;

namespace Arzly.Api.Mappings
{
    public static class CategoryMapping
    {
        public static CategoryResponse ToResponse(this Category entity)
        {
            return new CategoryResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                ItemsCount = entity.ItemsCount,
                ImageUrl = entity.ImageUrl,
            };
        }

        public static Category ToEntity(this CategoryAddRequest request)
        {
            return new Category
            {
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl
            };
        }

        public static Category ToEntity(this CategoryUpdateRequest request)
        {
            return new Category
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl
            };
        }

        public static CategoryUpdateRequest ToUpdateRequest(this CategoryResponse response)
        {
            return new CategoryUpdateRequest
            {
                Id = response.Id,
                Name = response.Name,
                Description = response.Description,
                ImageUrl = response.ImageUrl
            };
        }
    }
}
