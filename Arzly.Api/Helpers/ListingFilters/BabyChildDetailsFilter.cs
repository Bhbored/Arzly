using Arzly.Api.Domain.Entities;
using Arzly.Api.Domain.ListingOwned;

namespace Arzly.Api.Helpers.ListingFilters
{
    public static class BabyChildDetailsFilter
    {
        public static IQueryable<Listing> Apply(IQueryable<Listing> query, BabyChildDetails d)
        {
            if (d.AgeRange.HasValue)
                query = query.Where(x => x.BabyChildDetails!.AgeRange == d.AgeRange);
            if (d.Condition.HasValue)
                query = query.Where(x => x.BabyChildDetails!.Condition == d.Condition);
            if (d.StrollerSeatType.HasValue)
                query = query.Where(x => x.BabyChildDetails!.StrollerSeatType == d.StrollerSeatType);
            if (d.Gender.HasValue)
                query = query.Where(x => x.BabyChildDetails!.Gender == d.Gender);
            if (d.CribFurnitureType.HasValue)
                query = query.Where(x => x.BabyChildDetails!.CribFurnitureType == d.CribFurnitureType);
            if (d.FeedingType.HasValue)
                query = query.Where(x => x.BabyChildDetails!.FeedingType == d.FeedingType);

            return query;
        }
    }
}
