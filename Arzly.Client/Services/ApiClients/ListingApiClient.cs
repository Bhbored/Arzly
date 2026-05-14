using Arzly.Client.Services.Contracts;
using Arzly.Shared.DTOs.Response.Listing;

namespace Arzly.Client.Services.ApiClients;

public class ListingApiClient : BaseApiClient, IListingApiClient
{
    public ListingApiClient(HttpClient httpClient, ILogger<ListingApiClient> logger) 
        : base(httpClient, logger)
    {
    }

    public async Task<List<ListingResponse>> GetAllListingsAsync()
    {
        var result = await GetAsync<List<ListingResponse>>("arzly/admin/listingadmin/get-all");
        return result ?? new List<ListingResponse>();
    }

    public async Task<ListingResponse?> GetListingByIdAsync(Guid id)
    {
        return await GetAsync<ListingResponse>($"arzly/Listing/{id}");
    }
}
