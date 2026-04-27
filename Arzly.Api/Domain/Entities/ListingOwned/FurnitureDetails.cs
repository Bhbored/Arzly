using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.ListingOwned.HomeAndLiving;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.ListingOwned
{
    public class FurnitureDetails
    {
        [Key, ForeignKey(nameof(Listing))]
        public Guid ListingId { get; set; }
        public virtual Listing? Listing { get; set; }

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
