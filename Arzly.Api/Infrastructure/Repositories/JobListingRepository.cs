using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class JobListingRepository : BaseRepository<JobListing, Guid>, IJobListingRepository
    {
        public JobListingRepository(DbContext context) : base(context)
        {
        }
    }
}
