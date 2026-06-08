using Arzly.Api.Domain.Entities.Listings;

namespace Arzly.Api.Domain.Contracts.Listings
{
    public interface IJobListingRepository : IBaseRepository<JobListing, Guid>
    {
    }
}
