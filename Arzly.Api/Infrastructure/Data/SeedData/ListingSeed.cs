using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.Listing;
using Arzly.Shared.Enums.ListingOwned.PhonesAndGadgets;
using Arzly.Shared.Enums.ListingOwned.Motors;
using Arzly.Shared.Enums.ListingOwned.RealEstate;
using Arzly.Shared.Enums.JobListing;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class ListingSeed
    {
        public static readonly List<Listing> Data = new()
        {
            new Listing
            {
                Id = Guid.Parse("11b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"),
                OwnerId = "7c9e6679-7425-40de-944b-e07fc1f90ae7",
                CategoryId = Guid.Parse("e1b1a2c8-4e5f-4f6a-8b7c-9d0e1f2a3b4c"),
                SubcategoryId = Guid.Parse("81b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"),
                PickupLocationId = Guid.Parse("91b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"),
                Title = "iPhone 15 Pro",
                Description = "Excellent condition, 256GB",
                Price = 999.99,
                Status = ListingStatus.Active,
                Name = "John Doe",
                PhoneNumber = "1234567890",
                ContactMethod = ContactMethod.PhoneNumber,

            },
            new Listing
            {
                Id = Guid.Parse("12b2a2c4-5d4e-4f3a-9b8c-7d6e5f4a3b2c"),
                OwnerId = "8f3b2a1c-5d4e-4f3a-9b8c-7d6e5f4a3b2c",
                CategoryId = Guid.Parse("e2b2a2c9-5d4e-4f3a-9b8c-7d6e5f4a3b2c"),
                SubcategoryId = Guid.Parse("82b2a2c4-5d4e-4f3a-9b8c-7d6e5f4a3b2c"),
                PickupLocationId = Guid.Parse("92b2a2c4-5d4e-4f3a-9b8c-7d6e5f4a3b2c"),
                Title = "Tesla Model 3",
                Description = "Barely used, full self-driving",
                Price = 35000,
                Status = ListingStatus.Active,
                Name = "Jane Smith",
                PhoneNumber = "0987654321",
                ContactMethod = ContactMethod.InAppChat,

            },
            new Listing
            {
                Id = Guid.Parse("13b3a2c5-6f5e-4f2a-8b7c-9d0e1f2a3b4c"),
                OwnerId = "7c9e6679-7425-40de-944b-e07fc1f90ae7",
                CategoryId = Guid.Parse("e3b3a2ca-6f5e-4f2a-8b7c-9d0e1f2a3b4c"),
                SubcategoryId = Guid.Parse("83b3a2c5-6f5e-4f2a-8b7c-9d0e1f2a3b4c"),
                PickupLocationId = Guid.Parse("91b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"),
                Title = "Modern Apartment",
                Description = "2 bedrooms, city view",
                Price = 2500,
                Status = ListingStatus.Active,
                Name = "John Doe",
                PhoneNumber = "1234567890",
                ContactMethod = ContactMethod.PhoneNumber,

            }
        };
    }
}
