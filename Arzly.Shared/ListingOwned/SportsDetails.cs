using Arzly.Shared.Enums.ListingOwned.SportAndOutdoors;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Shared.ListingOwned
{
    [Owned]
    public class SportsDetails
    {
        
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