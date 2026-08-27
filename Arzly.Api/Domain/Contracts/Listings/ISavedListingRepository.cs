using Arzly.Api.Domain.Entities.Listings;

namespace Arzly.Api.Domain.Contracts.Listings
{
    public interface ISavedListingRepository 
    {
        Task<SavedListing?> GetByListingIdAsync(Guid listingId, Guid userId);
        Task<SavedListing?> GetByIdAsync(Guid id, Guid userId);
        Task<SavedListing> CreateAsync(SavedListing entity);
        Task<List<SavedListing>> GetByUserIdAsync(Guid userId);
        Task<bool> SoftDeleteAsync(Guid id, Guid userId);
        Task<bool> UndeleteAsync(Guid id, Guid userId);
    }
}
