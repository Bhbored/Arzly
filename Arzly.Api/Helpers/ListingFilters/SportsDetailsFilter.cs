using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Domain.ListingOwned;

namespace Arzly.Api.Helpers.ListingFilters
{
    public static class SportsDetailsFilter
    {
        public static IQueryable<Listing> Apply(IQueryable<Listing> query, SportsDetails d)
        {
            if (d.BicycleType.HasValue)
                query = query.Where(x => x.SportsDetails!.BicycleType == d.BicycleType);
            if (d.Condition.HasValue)
                query = query.Where(x => x.SportsDetails!.Condition == d.Condition);
            if (d.BicyclePowerType.HasValue)
                query = query.Where(x => x.SportsDetails!.BicyclePowerType == d.BicyclePowerType);
            if (d.OutdoorType.HasValue)
                query = query.Where(x => x.SportsDetails!.OutdoorType == d.OutdoorType);
            if (d.GymType.HasValue)
                query = query.Where(x => x.SportsDetails!.GymType == d.GymType);
            if (d.BallSportType.HasValue)
                query = query.Where(x => x.SportsDetails!.BallSportType == d.BallSportType);
            if (d.SupplementType.HasValue)
                query = query.Where(x => x.SportsDetails!.SupplementType == d.SupplementType);
            if (d.SupplementBrand.HasValue)
                query = query.Where(x => x.SportsDetails!.SupplementBrand == d.SupplementBrand);
            if (d.GameRoomType.HasValue)
                query = query.Where(x => x.SportsDetails!.GameRoomType == d.GameRoomType);
            if (d.WinterSportType.HasValue)
                query = query.Where(x => x.SportsDetails!.WinterSportType == d.WinterSportType);
            if (d.WaterSportType.HasValue)
                query = query.Where(x => x.SportsDetails!.WaterSportType == d.WaterSportType);
            if (d.RacketSportType.HasValue)
                query = query.Where(x => x.SportsDetails!.RacketSportType == d.RacketSportType);

            return query;
        }
    }
}
