using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Domain.ListingOwned;

namespace Arzly.Api.Helpers.ListingFilters
{
    public static class PhonesDetailsFilter
    {
        public static IQueryable<Listing> Apply(IQueryable<Listing> query, PhonesDetails d)
        {
            if (d.PhoneBrand.HasValue)
                query = query.Where(x => x.PhonesDetails!.PhoneBrand == d.PhoneBrand);
            if (d.PhoneCondition.HasValue)
                query = query.Where(x => x.PhonesDetails!.PhoneCondition == d.PhoneCondition);
            if (d.Storage.HasValue)
                query = query.Where(x => x.PhonesDetails!.Storage == d.Storage);
            if (d.Color.HasValue)
                query = query.Where(x => x.PhonesDetails!.Color == d.Color);
            if (d.AccessoryBrand.HasValue)
                query = query.Where(x => x.PhonesDetails!.AccessoryBrand == d.AccessoryBrand);
            if (d.AccessoryItemType.HasValue)
                query = query.Where(x => x.PhonesDetails!.AccessoryItemType == d.AccessoryItemType);
            if (d.Operator.HasValue)
                query = query.Where(x => x.PhonesDetails!.Operator == d.Operator);
            if (d.MembershipType.HasValue)
                query = query.Where(x => x.PhonesDetails!.MembershipType == d.MembershipType);
            if (d.WatchBrand.HasValue)
                query = query.Where(x => x.PhonesDetails!.WatchBrand == d.WatchBrand);
            if (d.WatchStorage.HasValue)
                query = query.Where(x => x.PhonesDetails!.WatchStorage == d.WatchStorage);
            if (d.BandMaterial.HasValue)
                query = query.Where(x => x.PhonesDetails!.BandMaterial == d.BandMaterial);
            if (d.BandColor.HasValue)
                query = query.Where(x => x.PhonesDetails!.BandColor == d.BandColor);

            return query;
        }
    }
}
