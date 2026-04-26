using Arzly.Shared.Enums.ListingOwned.RealEstate;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Shared.ListingOwned
{
    [Owned]
    public class RealEstateDetails
    {
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