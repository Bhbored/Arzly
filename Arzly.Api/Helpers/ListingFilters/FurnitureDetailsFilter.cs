using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Domain.ListingOwned;

namespace Arzly.Api.Helpers.ListingFilters
{
    public static class FurnitureDetailsFilter
    {
        public static IQueryable<Listing> Apply(IQueryable<Listing> query, FurnitureDetails d)
        {
            if (d.LivingRoomType.HasValue)
                query = query.Where(x => x.FurnitureDetails!.LivingRoomType == d.LivingRoomType);
            if (d.Condition.HasValue)
                query = query.Where(x => x.FurnitureDetails!.Condition == d.Condition);
            if (d.BedroomType.HasValue)
                query = query.Where(x => x.FurnitureDetails!.BedroomType == d.BedroomType);
            if (d.DiningRoomType.HasValue)
                query = query.Where(x => x.FurnitureDetails!.DiningRoomType == d.DiningRoomType);
            if (d.KitchenwareType.HasValue)
                query = query.Where(x => x.FurnitureDetails!.KitchenwareType == d.KitchenwareType);
            if (d.BathroomType.HasValue)
                query = query.Where(x => x.FurnitureDetails!.BathroomType == d.BathroomType);
            if (d.HomeDecorType.HasValue)
                query = query.Where(x => x.FurnitureDetails!.HomeDecorType == d.HomeDecorType);
            if (d.GardenType.HasValue)
                query = query.Where(x => x.FurnitureDetails!.GardenType == d.GardenType);

            return query;
        }
    }
}
