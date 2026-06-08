using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Domain.ListingOwned;

namespace Arzly.Api.Helpers.ListingFilters
{
    public static class FashionDetailsFilter
    {
        public static IQueryable<Listing> Apply(IQueryable<Listing> query, FashionDetails d)
        {
            if (d.MensClothingType.HasValue)
                query = query.Where(x => x.FashionDetails!.MensClothingType == d.MensClothingType);
            if (d.Condition.HasValue)
                query = query.Where(x => x.FashionDetails!.Condition == d.Condition);
            if (d.MensAccessoryType.HasValue)
                query = query.Where(x => x.FashionDetails!.MensAccessoryType == d.MensAccessoryType);
            if (d.WomensClothingType.HasValue)
                query = query.Where(x => x.FashionDetails!.WomensClothingType == d.WomensClothingType);
            if (d.WomensAccessoryType.HasValue)
                query = query.Where(x => x.FashionDetails!.WomensAccessoryType == d.WomensAccessoryType);
            if (d.CosmeticType.HasValue)
                query = query.Where(x => x.FashionDetails!.CosmeticType == d.CosmeticType);
            if (d.JewelryType.HasValue)
                query = query.Where(x => x.FashionDetails!.JewelryType == d.JewelryType);
            if (d.JewelryMaterial.HasValue)
                query = query.Where(x => x.FashionDetails!.JewelryMaterial == d.JewelryMaterial);
            if (d.WatchGender.HasValue)
                query = query.Where(x => x.FashionDetails!.WatchGender == d.WatchGender);

            return query;
        }
    }
}
