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

        public override async Task<PickupLocation> Update(PickupLocation entity)
        {
            var matchingLocation = await _db.PickupLocations
                .FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (matchingLocation is not null)
            {
                matchingLocation.Lat = entity.Lat;
                matchingLocation.Lon = entity.Lon;
                matchingLocation.Label = entity.Label;
                matchingLocation.Address = entity.Address;
                matchingLocation.Notes = entity.Notes;
                matchingLocation.IsDefault = entity.IsDefault;
                await _db.SaveChangesAsync();
                return matchingLocation;
            }
            return entity;

        }
        public async Task<List<PickupLocation>> GetByUserId(string userId)
        {
            return await _db.PickupLocations
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> SoftDeleteLocation(Guid id)
        {
            var entity = await _db.PickupLocations
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entity !=null)
            {
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;

            }
            var rows = await _db.SaveChangesAsync();
            return rows > 0;

        }
    }
}
