using Arzly.Api.Domain.Entities;
using Arzly.Api.Domain.ListingOwned;

namespace Arzly.Api.Helpers.ListingFilters
{
    public static class RealEstateDetailsFilter
    {
        public static IQueryable<Listing> Apply(IQueryable<Listing> query, RealEstateDetails d)
        {
            if (d.ListingType.HasValue)
                query = query.Where(x => x.RealEstateDetails!.ListingType == d.ListingType);
            if (d.PropertyType.HasValue)
                query = query.Where(x => x.RealEstateDetails!.PropertyType == d.PropertyType);
            if (d.Ownership.HasValue)
                query = query.Where(x => x.RealEstateDetails!.Ownership == d.Ownership);
            if (d.Bedrooms.HasValue)
                query = query.Where(x => x.RealEstateDetails!.Bedrooms == d.Bedrooms);
            if (d.Bathrooms.HasValue)
                query = query.Where(x => x.RealEstateDetails!.Bathrooms == d.Bathrooms);
            if (d.Size.HasValue)
                query = query.Where(x => x.RealEstateDetails!.Size <= d.Size);
            if (d.Furnished.HasValue)
                query = query.Where(x => x.RealEstateDetails!.Furnished == d.Furnished);
            if (d.Condition.HasValue)
                query = query.Where(x => x.RealEstateDetails!.Condition == d.Condition);
            if (d.Floor.HasValue)
                query = query.Where(x => x.RealEstateDetails!.Floor == d.Floor);
            if (d.PropertyAge.HasValue)
                query = query.Where(x => x.RealEstateDetails!.PropertyAge == d.PropertyAge);
            if (d.CommercialType.HasValue)
                query = query.Where(x => x.RealEstateDetails!.CommercialType == d.CommercialType);
            if (d.Equipped.HasValue)
                query = query.Where(x => x.RealEstateDetails!.Equipped == d.Equipped);
            if (d.LandType.HasValue)
                query = query.Where(x => x.RealEstateDetails!.LandType == d.LandType);
            if (d.ChaletType.HasValue)
                query = query.Where(x => x.RealEstateDetails!.ChaletType == d.ChaletType);
            if (d.RoomFurnished.HasValue)
                query = query.Where(x => x.RealEstateDetails!.RoomFurnished == d.RoomFurnished);

            return query;
        }
    }
}
