using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.ListingOwned.StyleAndWellness;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.ListingOwned
{
    public class FashionDetails
    {
        [Key, ForeignKey(nameof(Listing))]
        public Guid ListingId { get; set; }
        public virtual Listing? Listing { get; set; }

        public MensClothingType? MensClothingType { get; set; }
        public FashionCondition? Condition { get; set; }


        public MensAccessoryType? MensAccessoryType { get; set; }


        public WomensClothingType? WomensClothingType { get; set; }

        public WomensAccessoryType? WomensAccessoryType { get; set; }


        public CosmeticType? CosmeticType { get; set; }

        public JewelryType? JewelryType { get; set; }
        public JewelryMaterial? JewelryMaterial { get; set; }

        public WatchGender? WatchGender { get; set; }
    }
}
