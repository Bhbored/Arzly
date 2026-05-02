using Arzly.Api.Domain.Contracts;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Microsoft.EntityFrameworkCore;
namespace Arzly.Api.Infrastructure.Repositories
{
    public class ListingOwnedRepository : IListingOwnedRepository
    {
        private readonly AppDbContext _db;

        public ListingOwnedRepository(AppDbContext db) => _db = db;

        public async Task<object?> GetByListingId(Guid listingId)
        {
            var result = await GetByListingIds(new List<Guid> { listingId });
            return result.TryGetValue(listingId, out var detail) ? detail : null;
        }

        public async Task<Dictionary<Guid, object>> GetByListingIds(List<Guid> listingIds)
        {
            var result = new Dictionary<Guid, object>();

            var vehicles = await _db.VehiclesDetails.AsNoTracking().Where(x => listingIds.Contains(x.ListingId)).ToListAsync();
            foreach (var v in vehicles) result[v.ListingId] = v;

            var realEstate = await _db.RealEstateDetails.AsNoTracking().Where(x => listingIds.Contains(x.ListingId)).ToListAsync();
            foreach (var r in realEstate) result[r.ListingId] = r;

            var electronics = await _db.ElectronicsDetails.AsNoTracking().Where(x => listingIds.Contains(x.ListingId)).ToListAsync();
            foreach (var e in electronics) result[e.ListingId] = e;

            var furniture = await _db.FurnitureDetails.AsNoTracking().Where(x => listingIds.Contains(x.ListingId)).ToListAsync();
            foreach (var f in furniture) result[f.ListingId] = f;

            var phones = await _db.PhonesDetails.AsNoTracking().Where(x => listingIds.Contains(x.ListingId)).ToListAsync();
            foreach (var p in phones) result[p.ListingId] = p;

            var services = await _db.ServicesDetails.AsNoTracking().Where(x => listingIds.Contains(x.ListingId)).ToListAsync();
            foreach (var s in services) result[s.ListingId] = s;

            var pets = await _db.PetsDetails.AsNoTracking().Where(x => listingIds.Contains(x.ListingId)).ToListAsync();
            foreach (var p in pets) result[p.ListingId] = p;

            var fashion = await _db.FashionDetails.AsNoTracking().Where(x => listingIds.Contains(x.ListingId)).ToListAsync();
            foreach (var f in fashion) result[f.ListingId] = f;

            var babyChild = await _db.BabyChildDetails.AsNoTracking().Where(x => listingIds.Contains(x.ListingId)).ToListAsync();
            foreach (var b in babyChild) result[b.ListingId] = b;

            var hobbies = await _db.HobbiesDetails.AsNoTracking().Where(x => listingIds.Contains(x.ListingId)).ToListAsync();
            foreach (var h in hobbies) result[h.ListingId] = h;

            var sports = await _db.SportsDetails.AsNoTracking().Where(x => listingIds.Contains(x.ListingId)).ToListAsync();
            foreach (var s in sports) result[s.ListingId] = s;

            return result;
        }
    }
}
