using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;

namespace Arzly.Api.Mappings
{
    public static class ListingMapping
    {
        public static ListingResponse ToResponse(this Listing entity)
        {
            return new ListingResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Price = entity.Price,
                Status = entity.Status,
                PrimaryImageUrl = entity.PrimaryImageUrl,
                ImagesUrl = entity.ImagesUrl,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                OwnerId = entity.OwnerId,
                CategoryId = entity.CategoryId,
                SubcategoryId = entity.SubcategoryId,
                SubcategoryOptionsId = entity.SubcategoryOptionsId,
                PickupLocationId = entity.PickupLocationId
            };
        }

        public static Listing ToEntity(this ListingAddRequest request)
        {
            return new Listing
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Status = request.Status,
                PrimaryImageUrl = request.PrimaryImageUrl,
                ImagesUrl = request.ImagesUrl,
                OwnerId = request.OwnerId,
                CategoryId = request.CategoryId,
                SubcategoryId = request.SubcategoryId,
                SubcategoryOptionsId = request.SubcategoryOptionsId,
                PickupLocationId = request.PickupLocationId
            };
        }

        public static Listing ToEntity(this ListingUpdateRequest request)
        {
            return new Listing
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Status = request.Status,
                PrimaryImageUrl = request.PrimaryImageUrl,
                ImagesUrl = request.ImagesUrl,
                CategoryId = request.CategoryId,
                SubcategoryId = request.SubcategoryId,
                SubcategoryOptionsId = request.SubcategoryOptionsId,
                PickupLocationId = request.PickupLocationId
            };
        }

        public static ListingUpdateRequest ToUpdateRequest(this ListingResponse response)
        {
            return new ListingUpdateRequest
            {
                Id = response.Id,
                Title = response.Title,
                Description = response.Description,
                Price = response.Price,
                Status = response.Status,
                PrimaryImageUrl = response.PrimaryImageUrl,
                ImagesUrl = response.ImagesUrl,
                CategoryId = response.CategoryId,
                SubcategoryId = response.SubcategoryId,
                SubcategoryOptionsId = response.SubcategoryOptionsId,
                PickupLocationId = response.PickupLocationId
            };
        }
    }
}
