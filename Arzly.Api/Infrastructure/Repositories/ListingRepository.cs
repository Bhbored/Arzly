using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class ListingRepository : BaseRepository<Listing, Guid>, IListingRepository
    {
        public ListingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
