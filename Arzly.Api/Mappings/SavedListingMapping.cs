using Arzly.Api.Domain.Entities.Listings;
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

       

       
    }
}
