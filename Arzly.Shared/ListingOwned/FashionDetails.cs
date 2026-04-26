using Arzly.Shared.Enums.ListingOwned.StyleAndWellness;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Shared.ListingOwned
{
    [Owned]
    public class FashionDetails
    {

        public MensClothingType? MensClothingType { get; set; }
        public FashionCondition? Condition { get; set; }


        public MensAccessoryType? MensAccessoryType { get; set; }


        public WomensClothingType? WomensClothingType { get; set; }
        // Reuses Condition

        public WomensAccessoryType? WomensAccessoryType { get; set; }


        public CosmeticType? CosmeticType { get; set; }

        public JewelryType? JewelryType { get; set; }
        public JewelryMaterial? JewelryMaterial { get; set; }

        public WatchGender? WatchGender { get; set; }
    }
}