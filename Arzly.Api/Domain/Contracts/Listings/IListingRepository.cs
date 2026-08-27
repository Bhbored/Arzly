using Arzly.Api.Domain.Entities.Listings;
using Arzly.Shared.DTOs.Response.Listing;
using Arzly.Shared.Enums;
using Arzly.Shared.Enums.Listing;
using System.Linq.Expressions;
using System.Text.Json;

namespace Arzly.Api.Domain.Contracts.Listings
{
    public interface IListingRepository : IBaseRepository<Listing, Guid>
    {
        Task<List<Listing>> GetFilteredListing(Expression<Func<Listing, bool>> predicate, int pageSize, int currentPage,
            LocationPreset? preset, double minPrice, double maxPrice, string order, string orderByPrice);
        Task<List<string>> GetFilteredListingTitles(Expression<Func<Listing, bool>> predicate);

        Task<List<Listing>> GetListingByUserId(Guid id, int pageSize, int currentPage);
        Task<List<Listing>> GetIndexedListings(int pageSzie, int currentPage);
        Task<List<Listing>> GetListingByCategoryId(Guid categoryId, int pageSize, int currentPage, string? searchString,
            LocationPreset? preset, double minPrice, double maxPrice, string order, string orderByPrice);
        Task<List<Listing>> GetListingBySubCategoryId(Guid subcategoryId, int pageSize, int currentPage, string? searchString,
            LocationPreset? preset, object? details, double minPrice, double maxPrice, string order, string orderByPrice);
        Task<List<Listing>> GetInitialListings(Guid subcategoryId, LocationPreset? location);
        Task AddListingDetails(object details, Guid listingId);
        Task UpdateListingDetails(object details, Guid listingId);


            //admin
            Task<List<Listing>> GetAllListingAdmin(int pageSize, int currentPage);
            Task<Listing?> GetByIdAdminAsync(Guid id);
            Task<Listing?> SetStatusAdminAsync(Guid id, ListingStatus status);
            Task<Listing?> RejectAdminAsync(Guid id, string reason);
            Task<bool> DeleteAdminAsync(Guid id);
            Task<Listing?> RestoreAdminAsync(Guid id);
        Task<Listing> UpdateAdmin(Listing entity);
        Task<string?> GetTitleByIdAsync(Guid listingId);
    }
}
