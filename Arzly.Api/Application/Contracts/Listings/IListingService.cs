using Arzly.Api.Domain.Entities.Listings;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;
using Arzly.Shared.Enums;
using Arzly.Shared.Enums.Listing;

namespace Arzly.Api.Application.Contracts.Listings
{
    public interface IListingService : IBaseService<Listing, ListingResponse, ListingAddRequest, ListingUpdateRequest, Guid>
    {
        Task<List<ListingResponse>> GetListingBySubCategoryId(Guid subcategoryId, Guid categoryId, int pageSize, int currentPage, string? searchString,
            LocationPreset? preset, object? details, string order,string orderByPrice, double minPrice, double maxPrice);

        Task<List<ListingResponse>> GetListingByCategoryId(Guid categoryId, int pageSize, int currentPage, string? searchString,
            LocationPreset? preset, string order, string orderByPrice, double minPrice, double maxPrice);
        Task<List<ListingResponse>> GetFilteredListing(string searchBy, string searchString, LocationPreset? preset, string order,
            string orderByPrice, double minPrice, double maxPrice, int pageSize, int currentPage);
        Task<List<string>> GetFilteredListingTitles(string searchString);

        Task<List<ListingResponse>> GetListingByUserId(Guid? userId, int pageSize , int currentPage );
        Task<List<ListingResponse>> GetIndexedListings(int pageSzie , int currentPage);
        Task<List<ListingResponse>> GetInitialListings(List<string> subcategoriesTitle, LocationPreset? location);




        //admin 

        Task<List<ListingResponse>> GetAllListingAdmin(int pageSize, int currentPage);//id later with identity logic
        Task<ListingResponse> GetByIdAdminAsync(Guid id);
        Task<ListingResponse> SetStatusAdminAsync(Guid id, ListingStatus status, Guid actorId, string actorRole);
        Task<ListingResponse> RejectAdminAsync(Guid id, string reason, Guid actorId, string actorRole);
        Task DeleteAdminAsync(Guid id, Guid actorId, string actorRole);
        Task<ListingResponse> RestoreAdminAsync(Guid id, Guid actorId, string actorRole);
        Task<List<Arzly.Shared.DTOs.Response.UserActivityLog.UserActivityLogResponse>> GetModerationHistoryAsync(Guid id, int pageSize, int currentPage);
        Task<ListingResponse?> UpdateAsyncAdmin(ListingUpdateRequest? updateDto);
        Task<string?> GetTitleByIdAsync(Guid listingId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }
}
