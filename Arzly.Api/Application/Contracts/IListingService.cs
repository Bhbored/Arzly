using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;
using Arzly.Shared.Enums;

namespace Arzly.Api.Application.Contracts
{
    public interface IListingService : IBaseService<Listing, ListingResponse, ListingAddRequest, ListingUpdateRequest, Guid>
    {
        Task<List<ListingResponse>> GetListingBySubCategoryId(Guid subcategoryId, Guid categoryId, int pageSize, int currentPage, string? searchString,
            LocationPreset? preset, object? details, string order,string orderByPrice, double minPrice, double maxPrice);

        Task<List<ListingResponse>> GetListingByCategoryId(Guid categoryId, int pageSize, int currentPage, string? searchString,
            LocationPreset? preset, string order, string orderByPrice, double minPrice, double maxPrice);
        Task<List<ListingResponse>> GetFilteredListing(string searchBy, string searchString, int pageSize , int currentPage);

        Task<List<ListingResponse>> GetListingByUserId(string? userId, int pageSize , int currentPage );
        Task<List<ListingResponse>> GetIndexedListings(int pageSzie , int currentPage);
        Task<List<ListingResponse>> GetInitialListings(List<Guid> subcategoryIds);




        //admin 

        Task<List<ListingResponse>> GetAllListingAdmin(int pageSize, int currentPage);//id later with identity logic
        Task<ListingResponse?> UpdateAsyncAdmin(ListingUpdateRequest? updateDto);
    }
}
