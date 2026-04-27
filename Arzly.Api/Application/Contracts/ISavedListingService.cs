using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.SavedListing;
using Arzly.Shared.DTOs.Response.SavedListing;

namespace Arzly.Api.Application.Contracts
{
    public interface ISavedListingService : IBaseService<SavedListing, SavedListingResponse, SavedListingAddRequest, SavedListingUpdateRequest, Guid>
    {
    }
}
