using Arzly.Shared.Enums.ListingOwned.HomeAndLiving;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Shared.ListingOwned
{
    [Owned]
    public class FurnitureDetails
    {
   
        public LivingRoomType? LivingRoomType { get; set; }
        public FurnitureCondition? Condition { get; set; }

        public BedroomType? BedroomType { get; set; }

        public DiningRoomType? DiningRoomType { get; set; }
   
        public KitchenwareType? KitchenwareType { get; set; }
   
        public BathroomType? BathroomType { get; set; }

        public HomeDecorType? HomeDecorType { get; set; }

        public GardenType? GardenType { get; set; }
    }
}