using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class ListingRepository : BaseRepository<Listing, Guid>, IListingRepository
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ListingRepository> _logger;
        public ListingRepository(AppDbContext context, ILogger<ListingRepository> logger) : base(context)
        {

            _db = context;
            _logger = logger;
        }

        
        public override async Task<Listing?> GetByIdAsync(Guid id)
        {
            return await _db.Listings
                .Include(l => l.PickupLocation)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public override async Task<List<Listing>> GetAllAsync()
        {

            return await _db.Listings
                   .Include(l => l.PickupLocation)
                   .ToListAsync();
        }

        public async Task<List<Listing>> GetFilteredListing(Expression<Func<Listing, bool>> predicate)
        {
            _logger.LogInformation("GetFilteredListing in the Repo has been reached");

            return await _db.Listings
                .Where(predicate)
                .Include(l => l.PickupLocation)
                .ToListAsync();
        }

        public async Task<List<Listing>> GetListingByUserId(string id)
        {
            return await _db.Listings
                .Where(l => l.OwnerId == id)
                .Include(l => l.PickupLocation)
                .ToListAsync();
        }
        public async Task<List<Listing>> GetIndexedListings(int pageSzie, int currentPage)
        {
            return await _db.Listings
                .Skip(currentPage * pageSzie)
                .Take(pageSzie)
                .Include(l => l.PickupLocation)
                .ToListAsync();
        }

        public override Task AddAsync(Listing entity)
        {
            return base.AddAsync(entity);
        }


    }
}
