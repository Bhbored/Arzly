using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;

namespace Arzly.Api.Application.Contracts
{
    public interface IListingService : IBaseService<Listing, ListingResponse, ListingAddRequest, ListingUpdateRequest, Guid>
    {
        Task<List<ListingResponse>> GetListingByCategoryId(Guid? categoryId);

        Task<List<ListingResponse>> GetFilteredListing(string searchBy, string searchString);

        Task<List<ListingResponse>> GetListingByUserId(string? userId);
        Task<List<ListingResponse>> GetIndexedListings(int pageSzie = 10, int currentPage=0);


    }
}
