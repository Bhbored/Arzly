using Arzly.Api.Domain.ListingOwned;
using Arzly.Shared.Enums.ListingOwned.RealEstate;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class RealEstateDetailsSeed
    {
        public static readonly List<RealEstateDetails> Data = new()
        {
            new RealEstateDetails
            {
                ListingId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                ListingType = PropertyListingType.ForRent,
                ReferenceId = "RE-00003",
                PropertyType = PropertyType.Apartment,
                Ownership = OwnershipType.ByOwner,
                Bedrooms = Bedrooms.Two,
                Bathrooms = 2,
                Size = 95.0,
                Furnished = FurnishedStatus.Unfurnished,
                Condition = PropertyCondition.ReadyToMoveIn,
                Floor = FloorLevel.Fifth,
                Features = new List<ApartmentFeature> { ApartmentFeature.Gym, ApartmentFeature.Pool, ApartmentFeature.LaundryRoom },
                PropertyAge = PropertyAge.OneToFiveYears,
            },
        };
    }
}
