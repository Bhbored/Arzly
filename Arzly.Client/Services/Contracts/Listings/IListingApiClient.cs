using Arzly.Shared.DTOs.Response.Listing;
using Arzly.Shared.DTOs.Request.Listing;

namespace Arzly.Client.Services.Contracts.Listings;

public interface IListingApiClient
{
    Task<List<ListingResponse>> GetAllListingsAsync();
    Task<ListingResponse?> GetListingByIdAsync(Guid id);
    Task<ListingResponse?> UpdateListingAsync(ListingUpdateRequest request);
}
