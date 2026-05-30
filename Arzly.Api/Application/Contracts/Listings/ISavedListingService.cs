using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.SavedListing;
using Arzly.Shared.DTOs.Response.SavedListing;

namespace Arzly.Api.Application.Contracts.Listings
{
    public interface ISavedListingService : IBaseService<SavedListing, SavedListingResponse, SavedListingAddRequest, SavedListingUpdateRequest, Guid>
    {
    }
}
