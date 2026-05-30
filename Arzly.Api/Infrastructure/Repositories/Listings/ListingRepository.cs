using Arzly.Api.Domain.Contracts.Listings;
﻿using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Contracts.Listings;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Domain.ListingOwned;
using Arzly.Api.Helpers;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Shared.Enums;
using Arzly.Shared.Enums.Listing;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text.Json;

namespace Arzly.Api.Infrastructure.Repositories.Listings
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


        #region admin & support

        public async Task<List<Listing>> GetAllListingAdmin(int pageSize, int currentPage)
        {
            _logger.LogInformation($"{GetType().Name} - GetAllListingAdmin has been reached");

            return await _db.Listings
                   .Skip(currentPage * pageSize)
                   .Include(l => l.PickupLocation)
                   .ToListAsync();
        }

        public async Task<Listing> UpdateAdmin(Listing entity)
        {
            _logger.LogInformation($"{GetType().Name} - UpdateAdmin has been reached");

            var olderListing = await _db.Listings
                .FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (olderListing != null)
            {

                olderListing.Status = entity.Status;
                olderListing.IsPromoted = entity.IsPromoted;
                olderListing.PromotionType = entity.PromotionType;
                olderListing.UpdatedAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();
                return olderListing;
            }

            return entity;
        }




        #endregion


        #region user
        #endregion
        public override async Task<Listing?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation($"{GetType().Name} - GetByIdAsync has been reached");

            return await _db.Listings
                .Include(l => l.PickupLocation)
                .FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<List<Listing>> GetFilteredListing(Expression<Func<Listing, bool>> predicate, int pageSize, int currentPage)
        {
            _logger.LogInformation($"{GetType().Name} - GetFilteredListing has been reached");

            return await _db.Listings
                .Where(predicate)
                .Where(x => x.Status == ListingStatus.Active)
                .Skip(currentPage * pageSize)
                .Take(pageSize)
                .Include(l => l.PickupLocation)
                .ToListAsync();
        }

        public async Task<List<Listing>> GetListingByUserId(Guid id, int pageSize, int currentPage)
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
                .Where(x => x.Status == ListingStatus.Active)
                .Skip(currentPage * pageSzie)
                .Take(pageSzie)
                .Include(l => l.PickupLocation)
                .ToListAsync();
        }

        public async Task<List<Listing>> GetListingByCategoryId(Guid categoryId, int pageSize, int currentPage, string? searchString,
      LocationPreset? preset, double minPrice, double maxPrice, string order, string orderByPrice)
        {
            IQueryable<Listing> query = _db.Listings
                .Where(x => x.CategoryId == categoryId && x.Status == ListingStatus.Active)
                .Where(x => x.Price >= minPrice && x.Price <= maxPrice);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x => x.Title.Contains(searchString));
            }

            if (preset != null)
            {
                query = query.Where(x => x.PickupLocation.LocationPreset == preset);
            }

            IOrderedQueryable<Listing> orderedQuery;

            if (order.Equals("desc"))
            {
                orderedQuery = query.OrderByDescending(x => x.CreatedAt);
            }
            else
            {
                orderedQuery = query.OrderBy(x => x.CreatedAt);
            }

            if (orderByPrice.Equals("desc"))
            {
                orderedQuery = orderedQuery.ThenByDescending(x => x.Price);
            }
            else
            {
                orderedQuery = orderedQuery.ThenBy(x => x.Price);
            }

            return await orderedQuery
                .Skip(currentPage * pageSize)
                .Take(pageSize)
                .Include(x => x.PickupLocation)
                .ToListAsync();
        }

        public async Task<List<Listing>> GetListingBySubCategoryId(Guid subcategoryId, int pageSize, int currentPage, string? searchString,
            LocationPreset? preset, object? details, double minPrice, double maxPrice, string order, string orderByPrice)
        {
            var query = _db.Listings
        .Where(x => x.SubcategoryId == subcategoryId && x.Status == ListingStatus.Active)
        .Where(x => x.Price >= minPrice && x.Price <= maxPrice);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(x => x.Title.Contains(searchString));
            }

            if (preset != null)
            {
                query = query.Where(x => x.PickupLocation.LocationPreset == preset);
            }

            if (details != null)
                query = ListingFilterHelper.Apply(query, details);

            IOrderedQueryable<Listing> orderedQuery;

            if (order.Equals("desc"))
            {
                orderedQuery = query.OrderByDescending(x => x.CreatedAt);
            }
            else
            {
                orderedQuery = query.OrderBy(x => x.CreatedAt);
            }

            if (orderByPrice.Equals("desc"))
            {
                orderedQuery = orderedQuery.ThenByDescending(x => x.Price);
            }
            else
            {
                orderedQuery = orderedQuery.ThenBy(x => x.Price);
            }

            return await orderedQuery
                .Skip(currentPage * pageSize)
                .Take(pageSize)
                .Include(x => x.PickupLocation)
                .ToListAsync();

        }

        public async Task<List<Listing>> GetInitialListings(Guid subcategoryId)
        {
            return await _db.Listings
                              .Where(x => x.SubcategoryId == subcategoryId && x.Status == ListingStatus.Active)
                              .OrderByDescending(x => x.IsPromoted)
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
