using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Infrastructure.Data.DataBaseContext;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class SavedListingRepository : BaseRepository<SavedListing, Guid>, ISavedListingRepository
    {
        public SavedListingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
