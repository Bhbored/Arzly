using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Domain.Contracts.Listings
{
    public interface IJobListingRepository : IBaseRepository<JobListing, Guid>
    {
    }
}
