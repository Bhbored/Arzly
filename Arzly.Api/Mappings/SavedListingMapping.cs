using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.SavedListing;
using Arzly.Shared.DTOs.Response.SavedListing;

namespace Arzly.Api.Mappings
{
    public static class SavedListingMapping
    {
        public static SavedListingResponse ToResponse(this SavedListing entity)
        {
            return new SavedListingResponse
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ListingId = entity.ListingId,
                SavedAt = entity.SavedAt,
                DeletedAt = entity.DeletedAt
            };
        }

        public static SavedListing ToEntity(this SavedListingAddRequest request)
        {
            return new SavedListing
            {
                UserId = request.UserId,
                ListingId = request.ListingId
            };
        }

        public static SavedListing ToEntity(this SavedListingUpdateRequest request)
        {
            return new SavedListing
            {
                Id = request.Id,
                DeletedAt = request.DeletedAt
            };
        }

        public static SavedListingUpdateRequest ToUpdateRequest(this SavedListingResponse response)
        {
            return new SavedListingUpdateRequest
            {
                Id = response.Id,
                DeletedAt = response.DeletedAt
            };
        }
    }
}
