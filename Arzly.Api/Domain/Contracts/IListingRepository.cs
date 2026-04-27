using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Domain.Contracts
{
    public interface IListingRepository : IBaseRepository<Listing, Guid>
    {
        // Add listing-specific queries here if needed
    }
}
