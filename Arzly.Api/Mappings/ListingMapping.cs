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

        public static Listing ToEntity(this ListingUpdateRequest request, Listing entity)
        {
            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.Price = request.Price;
            entity.PrimaryImageUrl = request.PrimaryImageUrl;
            entity.ImagesUrl = request.ImagesUrl;
            entity.CategoryId = request.CategoryId;
            entity.SubcategoryId = request.SubcategoryId;
            entity.PickupLocationId = request.PickupLocationId;
            entity.Name = request.Name;
            entity.PhoneNumber = request.PhoneNumber;
            entity.IsPriceNegotiable = request.IsPriceNegotiable;
            entity.ContactMethod = request.ContactMethod;
            entity.UpdatedAt = DateTime.UtcNow;

            return entity;
        }
    }
}