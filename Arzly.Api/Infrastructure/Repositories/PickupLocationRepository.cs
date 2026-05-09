using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class PickupLocationRepository : BaseRepository<PickupLocation, Guid>, IPickupLocationRepository
    {
        private readonly AppDbContext _db;
        public PickupLocationRepository(AppDbContext context) : base(context)
        {
            _db = context;
        }

        public async Task<List<PickupLocation>> GetByUserId(string userId)
        {
            return await _db.PickupLocations
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}
