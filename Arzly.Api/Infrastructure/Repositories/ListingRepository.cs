using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Domain.ListingOwned;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Shared.Enums.Listing;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text.Json;

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
            _logger.LogInformation($"{GetType().Name} - GetByIdAsync has been reached");

            return await _db.Listings
                .Include(l => l.PickupLocation)
                .FirstOrDefaultAsync(x => x.Id == id && x.Status == ListingStatus.Active || x.Status == ListingStatus.Sold);
        }
        public override async Task<List<Listing>> GetAllAsync()
        {
            _logger.LogInformation($"{GetType().Name} - GetAllAsync has been reached");

            return await _db.Listings
                   .Include(l => l.PickupLocation)
                   .ToListAsync();
        }

        public async Task<List<Listing>> GetFilteredListing(Expression<Func<Listing, bool>> predicate, int pageSize, int currentPage)
        {
            _logger.LogInformation($"{GetType().Name} - GetFilteredListing has been reached");

            return await _db.Listings
                .Where(predicate)
                .Where(x=> x.Status == ListingStatus.Active || x.Status == ListingStatus.Sold)
                .Skip(currentPage * pageSize)
                .Take(pageSize)
                .Include(l => l.PickupLocation)
                .ToListAsync();
        }

        public async Task<List<Listing>> GetListingByUserId(string id, int pageSize, int currentPage)
        {
            _logger.LogInformation($"{GetType().Name} - GetListingByUserId has been reached");

            return await _db.Listings
                .Where(l => l.OwnerId == id)
                .Skip(currentPage * pageSize)
                .Take(pageSize)
                .Include(l => l.PickupLocation)
                .ToListAsync();
        }
        public async Task<List<Listing>> GetIndexedListings(int pageSzie, int currentPage)
        {
            _logger.LogInformation($"{GetType().Name} - GetIndexedListings has been reached");

            return await _db.Listings
                .Where(x=>x.Status == ListingStatus.Active || x.Status == ListingStatus.Sold)
                .Skip(currentPage * pageSzie)
                .Take(pageSzie)
                .Include(l => l.PickupLocation)
                .ToListAsync();
        }

        public async Task<List<Listing>> GetListingByCategoryId(Guid categoryId, int pageSize, int currentPage)
        {
            return await _db.Listings 
                             .Where(x => x.CategoryId == categoryId && x.Status == ListingStatus.Active || x.Status == ListingStatus.Sold)
                             .Skip(currentPage * pageSize)
                             .Take(pageSize)
                             .Include(x => x.PickupLocation)
                             .ToListAsync();
        }

        public async Task<List<Listing>> GetInitialListings(Guid categoryId)
        {
            return await _db.Listings
                              .Where(x => x.CategoryId == categoryId && x.Status == ListingStatus.Active || x.Status == ListingStatus.Sold)
                              .OrderByDescending(x=>x.IsPromoted)
                              .Take(5)
                              .Include(x => x.PickupLocation)
                              .ToListAsync();
        }

        public async Task AddListingDetails(object details, Guid listingId)
        {
            var listingIdProperty = details.GetType().GetProperty("ListingId");
            if (listingIdProperty != null)
            {
                listingIdProperty.SetValue(details, listingId);
            }
            await _db.AddAsync(details);
            await _db.SaveChangesAsync();
        }
        public override Task AddAsync(Listing entity)
        {
            _logger.LogInformation($"{GetType().Name} - AddAsync has been reached");

            return base.AddAsync(entity);
        }
        //promotion and status (active rejected ) update will be another admin support endpoint
        public override async Task<Listing> Update(Listing entity)
        {
            var olderListing = await _db.Listings
                .FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (olderListing != null)
            {
                olderListing.Title = entity.Title;
                olderListing.Description = entity.Description;
                olderListing.Price = entity.Price;
                olderListing.Status = entity.Status;//just the sold option
                olderListing.PrimaryImageUrl = entity.PrimaryImageUrl;
                olderListing.ImagesUrl = entity.ImagesUrl;
                olderListing.PickupLocationId = entity.PickupLocationId;
                olderListing.Name = entity.Name;
                olderListing.PhoneNumber = entity.PhoneNumber;
                olderListing.IsPriceNegotiable = entity.IsPriceNegotiable;
                olderListing.ContactMethod = entity.ContactMethod;
                olderListing.UpdatedAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();
                return olderListing;
            }

            return entity;
        }
        public async override Task<bool> Delete(Listing entity)
        {
            var olderListing = await _db.Listings
               .FirstOrDefaultAsync(x => x.Id == entity.Id);
            olderListing?.IsDeleted = true;
            return true;
        }

    }
}
