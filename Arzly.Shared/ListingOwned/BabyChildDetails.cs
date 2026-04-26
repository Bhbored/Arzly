using Arzly.Shared.Enums.ListingOwned.BabyAndChild;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Shared.ListingOwned
{
    [Owned]
    public class BabyChildDetails
    {

        public KidsAgeRange? AgeRange { get; set; }
        public BabyChildCondition? Condition { get; set; }


        public StrollerSeatType? StrollerSeatType { get; set; }

        public KidGender? Gender { get; set; }

        public CribFurnitureType? CribFurnitureType { get; set; }


        public FeedingType? FeedingType { get; set; }
    }
}