using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Infrastructure.Data.DataBaseContext;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class ListingRepository : BaseRepository<Listing, Guid>, IListingRepository
    {
        public ListingRepository(AppDbContext context) : base(context)
        {
        }
        public override async Task<Listing?> GetByIdAsync(Guid id)
        {
            return await base.GetByIdAsync(id);
        }
    }
}
