using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Infrastructure.Data.DataBaseContext;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class PickupLocationRepository : BaseRepository<PickupLocation, Guid>, IPickupLocationRepository
    {
        public PickupLocationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
