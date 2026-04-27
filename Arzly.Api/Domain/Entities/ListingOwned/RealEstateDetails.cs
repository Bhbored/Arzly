using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.ListingOwned.RealEstate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.ListingOwned
{
    public class RealEstateDetails
    {
        [Key, ForeignKey(nameof(Listing))]
        public Guid ListingId { get; set; }
        public virtual Listing? Listing { get; set; }

        public PropertyListingType? ListingType { get; set; }
        public string? ReferenceId { get; set; }
        public PropertyType? PropertyType { get; set; }
        public OwnershipType? Ownership { get; set; }
        public Bedrooms? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public double? Size { get; set; }
        public FurnishedStatus? Furnished { get; set; }
        public PropertyCondition? Condition { get; set; }
        public FloorLevel? Floor { get; set; }
        public List<ApartmentFeature>? Features { get; set; }
        public PropertyAge? PropertyAge { get; set; }
        public CommercialType? CommercialType { get; set; }
        public bool? Equipped { get; set; }
        public List<CommercialFeature>? CommercialFeatures { get; set; }
        public LandType? LandType { get; set; }
        public ChaletType? ChaletType { get; set; }
        public List<ChaletFeature>? ChaletFeatures { get; set; }
        public bool? RoomFurnished { get; set; }
        public List<RoomFeature>? RoomFeatures { get; set; }
    }
}
