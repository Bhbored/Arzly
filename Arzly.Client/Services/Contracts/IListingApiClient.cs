using Arzly.Shared.DTOs.Response.Listing;

namespace Arzly.Client.Services.Contracts;

public interface IListingApiClient
{
    Task<List<ListingResponse>> GetAllListingsAsync();
    Task<ListingResponse?> GetListingByIdAsync(Guid id);
}
