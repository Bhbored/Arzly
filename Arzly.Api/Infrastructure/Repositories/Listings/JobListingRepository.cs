using Arzly.Api.Domain.Contracts.Listings;

using Arzly.Api.Domain.Entities.Listings;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories.Listings
{
    public class JobListingRepository : BaseRepository<JobListing, Guid>, IJobListingRepository
    {
        public JobListingRepository(DbContext context) : base(context)
        {
        }
    }
}
