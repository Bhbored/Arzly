using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;
using Arzly.Shared.Enums.Listing;

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
                IsPriceNegotiable = entity.IsPriceNegotiable,
                Status = entity.Status,
                PrimaryImageUrl = entity.PrimaryImageUrl,
                ImagesUrl = entity.ImagesUrl,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                OwnerId = entity.OwnerId,
                CategoryId = entity.CategoryId,
                SubcategoryId = entity.SubcategoryId,
                PickupLocationId = entity.PickupLocationId,
                Name = entity.Name,
                PhoneNumber = entity.PhoneNumber,
                ContactMethod = entity.ContactMethod,
                IsPromoted = entity.IsPromoted,
                PromotionType = entity.PromotionType,
                PromotionStartDate = entity.PromotionStartDate,
                PromotionEndDate = entity.PromotionEndDate,
            };
        }

        public static Listing ToEntity(this ListingAddRequest request)
        {
            return new Listing
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                PrimaryImageUrl = request.PrimaryImageUrl,
                ImagesUrl = request.ImagesUrl,
                CategoryId = request.CategoryId,
                SubcategoryId = request.SubcategoryId,
                PickupLocationId = request.PickupLocationId,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                IsPriceNegotiable = request.IsPriceNegotiable,
                ContactMethod = request.ContactMethod,

            };
        }

        public static Listing ToEntity(this ListingUpdateRequest request)
        {
            return new Listing
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                PrimaryImageUrl = request.PrimaryImageUrl,
                ImagesUrl = request.ImagesUrl,
                CategoryId = request.CategoryId,
                SubcategoryId = request.SubcategoryId,
                PickupLocationId = request.PickupLocationId,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                IsPriceNegotiable = request.IsPriceNegotiable,
                ContactMethod = request.ContactMethod,
                UpdatedAt = DateTime.UtcNow,
                Status = request.Status ?? ListingStatus.Pending,
                IsPromoted = request.IsPromoted ?? false,
                PromotionType = request.PromotionType,
                PromotionStartDate = request.PromotionStartDate,
                PromotionEndDate = request.PromotionEndDate,
            };
        }
    }
}