using Arzly.Api.Domain.Contracts.Listings;
using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Google;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories.Listings
{
    public class SavedListingRepository : ISavedListingRepository
    {
        private readonly AppDbContext _dbSet;
        public SavedListingRepository(AppDbContext context)
        {
            _dbSet = context;
        }

        public async Task<SavedListing?> GetByIdAsync(Guid id, Guid userId)
        {
            return await _dbSet.SavedListings
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);
        }
        public async Task<SavedListing?> GetByListingIdAsync(Guid listingId, Guid userId)
        {
            return await _dbSet.SavedListings
               .IgnoreQueryFilters()
               .FirstOrDefaultAsync(s => s.ListingId == listingId && s.UserId == userId);
        }

        public async Task<SavedListing> CreateAsync(SavedListing entity)
        {
            _dbSet.SavedListings.Add(entity);
            await _dbSet.SaveChangesAsync();
            return entity;
        }

        public async Task<List<SavedListing>> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet.SavedListings
                .AsNoTracking()
                .Where(s => s.UserId == userId)
                .OrderByDescending(x=>x.SavedAt)
                .ToListAsync();
        }

        public async Task<bool> SoftDeleteAsync(Guid id, Guid userId)
        {
            var entity = await _dbSet.SavedListings
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (entity is null) return false;

            entity.DeletedAt = DateTime.UtcNow;
            await _dbSet.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UndeleteAsync(Guid id, Guid userId)
        {
            var entity = await _dbSet.SavedListings
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if (entity is null) return false;

            entity.DeletedAt = null;
            await _dbSet.SaveChangesAsync();
            return true;
        }

        
    }
}
