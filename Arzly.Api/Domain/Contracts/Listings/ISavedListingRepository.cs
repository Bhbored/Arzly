using Arzly.Api.Domain.Entities.Listings;

namespace Arzly.Api.Domain.Contracts.Listings
{
    public interface ISavedListingRepository : IBaseRepository<SavedListing, Guid>
    {
    }
}
