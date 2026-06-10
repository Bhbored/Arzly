using Arzly.Api.Domain.Entities.Listings;

namespace Arzly.Api.Domain.Contracts.Listings
{
    public interface ISavedListingRepository 
    {
        Task<SavedListing?> GetByListingIdAsync(Guid listingId);
        Task<SavedListing?> GetByIdAsync(Guid id);
        Task<SavedListing> CreateAsync(SavedListing entity);
        Task<List<SavedListing>> GetByUserIdAsync(Guid userId);
        Task<bool> SoftDeleteAsync(Guid id);
        Task<bool> UndeleteAsync(Guid id);
    }
}
