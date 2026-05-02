using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.ListingOwned.SportAndOutdoors;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Arzly.Api.Domain.ListingOwned
{
    public class SportsDetails
    {
        [Key, ForeignKey(nameof(Listing))]
        public Guid ListingId { get; set; }
        [JsonIgnore]
        public virtual Listing? Listing { get; set; }

        public BicycleType? BicycleType { get; set; }
        public SportsCondition? Condition { get; set; }
        public BicyclePowerType? BicyclePowerType { get; set; }


        public OutdoorType? OutdoorType { get; set; }


        public GymType? GymType { get; set; }


        public BallSportType? BallSportType { get; set; }


        public SupplementType? SupplementType { get; set; }
        public SupplementBrand? SupplementBrand { get; set; }


        public GameRoomType? GameRoomType { get; set; }


        public WinterSportType? WinterSportType { get; set; }

        public WaterSportType? WaterSportType { get; set; }

        public RacketSportType? RacketSportType { get; set; }
    }
}
