using Arzly.Client.Services.Contracts;
using Arzly.Shared.DTOs.Request.Listing;
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
        var header = new Dictionary<string, string>()
        {
            ["pageSize"] = "10",
            ["currentPage"] = "0"
        };

        var result = await GetAsync<List<ListingResponse>>("arzly/admin/listingadmin/get-all", header);
        return result ?? new List<ListingResponse>();
    }

    public async Task<ListingResponse?> GetListingByIdAsync(Guid id)
    {
        return await GetAsync<ListingResponse>($"arzly/admin/listingadmin/{id}");//didn't implement yet
    }
    public async Task<ListingResponse?> UpdateListingAsync(ListingUpdateRequest request)
    {
        
        return await PutAsync<ListingUpdateRequest, ListingResponse>("arzly/admin/Update", request) ?? null;
    }

}
