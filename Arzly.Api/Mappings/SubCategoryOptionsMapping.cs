using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.SubCategoryOptions;
using Arzly.Shared.DTOs.Response.SubCategoryOptions;

namespace Arzly.Api.Mappings
{
    public static class SubCategoryOptionsMapping
    {
        public static SubCategoryOptionsResponse ToResponse(this SubCategoryOptions entity)
        {
            return new SubCategoryOptionsResponse
            {
                Id = entity.Id,
                SubCategoryId = entity.SubCategoryId,
                Name = entity.Name,
                Description = entity.Description,
                ItemsCount = entity.ItemsCount
            };
        }

        public static SubCategoryOptions ToEntity(this SubCategoryOptionsAddRequest request)
        {
            return new SubCategoryOptions
            {
                SubCategoryId = request.SubCategoryId,
                Name = request.Name,
                Description = request.Description
            };
        }

        public static SubCategoryOptions ToEntity(this SubCategoryOptionsUpdateRequest request)
        {
            return new SubCategoryOptions
            {
                Id = request.Id,
                SubCategoryId = request.SubCategoryId,
                Name = request.Name,
                Description = request.Description
            };
        }

        public static SubCategoryOptionsUpdateRequest ToUpdateRequest(this SubCategoryOptionsResponse response)
        {
            return new SubCategoryOptionsUpdateRequest
            {
                Id = response.Id,
                SubCategoryId = response.SubCategoryId,
                Name = response.Name,
                Description = response.Description
            };
        }
    }
}
