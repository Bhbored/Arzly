using Arzly.Api.Domain.Entities;
using Arzly.Api.Domain.ListingOwned;
using Arzly.Shared.Enums.JobListing;
using Arzly.Shared.Enums.Listing;
using Arzly.Shared.Enums.ListingOwned.Motors;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class ListingSeed
    {
        public static readonly List<Listing> Data = new()
        {
            // Vehicle - Car For Sale
            new Listing
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Title = "BMW 320i Luxury Line 2022",
                Description = "Well-maintained BMW 320i with full service history. Luxury package, navigation system, leather seats, and sunroof. Single owner, garage kept.",
                Price = 45000,
                Status = ListingStatus.Active,
                PrimaryImageUrl = "https://example.com/images/bmw-320i-1.jpg",
                ImagesUrl = new List<string> { "https://example.com/images/bmw-320i-2.jpg", "https://example.com/images/bmw-320i-3.jpg" },
                Name = "John Doe",
                PhoneNumber = "+1-555-0101",
                IsPriceNegotiable = true,
                ContactMethod = ContactMethod.InAppChat,
                OwnerId = "user-1-id",
                CategoryId = CategorySeed.Data[0].Id,
                SubcategoryId = SubCategorySeed.Data[0].Id,
                PickupLocationId = PickupLocationSeed.Data[0].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-15),
                
            },
            // Vehicle - Car For Sale
            new Listing
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Title = "Toyota Camry 2020 - Excellent Condition",
                Description = "Clean Toyota Camry, perfect for daily commute. Fuel-efficient, reliable, and spacious. Recent oil change and tire rotation.",
                Price = 28000,
                Status = ListingStatus.Active,
                PrimaryImageUrl = "https://example.com/images/toyota-camry-1.jpg",
                ImagesUrl = new List<string> { "https://example.com/images/toyota-camry-2.jpg" },
                Name = "Bourhan Hassoun",
                PhoneNumber = "+1-555-0102",
                IsPriceNegotiable = false,
                ContactMethod = ContactMethod.InAppChat,
                OwnerId = "user-2-id",
                CategoryId = CategorySeed.Data[0].Id,
                SubcategoryId = SubCategorySeed.Data[0].Id,
                PickupLocationId = PickupLocationSeed.Data[2].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
               
            },
            // Real Estate - Apartment For Rent
            new Listing
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Title = "Modern 2BR Apartment in Manhattan",
                Description = "Stunning 2-bedroom apartment with city views. Fully equipped kitchen, in-unit laundry, gym and pool in building. Close to subway.",
                Price = 4500,
                Status = ListingStatus.Active,
                PrimaryImageUrl = "https://example.com/images/apt-manhattan-1.jpg",
                ImagesUrl = new List<string> { "https://example.com/images/apt-manhattan-2.jpg", "https://example.com/images/apt-manhattan-3.jpg", "https://example.com/images/apt-manhattan-4.jpg" },
                Name = "John Doe",
                PhoneNumber = "+1-555-0103",
                IsPriceNegotiable = true,
                ContactMethod = ContactMethod.InAppChat,
                OwnerId = "user-1-id",
                CategoryId = CategorySeed.Data[1].Id,
                SubcategoryId = SubCategorySeed.Data[7].Id,
                PickupLocationId = PickupLocationSeed.Data[0].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            },
            // Electronics - Laptop
            new Listing
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                Title = "MacBook Pro 16\" M2 Max 2023",
                Description = "Like-new MacBook Pro with M2 Max chip, 32GB RAM, 1TB SSD. Perfect for professionals. AppleCare+ until 2026.",
                Price = 2800,
                Status = ListingStatus.Active,
                PrimaryImageUrl = "https://example.com/images/macbook-pro-1.jpg",
                ImagesUrl = new List<string> { "https://example.com/images/macbook-pro-2.jpg" },
                Name = "Bourhan Hassoun",
                PhoneNumber = "+1-555-0104",
                IsPriceNegotiable = false,
                ContactMethod = ContactMethod.PhoneNumber,
                OwnerId = "user-2-id",
                CategoryId = CategorySeed.Data[3].Id,
                SubcategoryId = SubCategorySeed.Data[26].Id,
                PickupLocationId = PickupLocationSeed.Data[2].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                IsPromoted = true,
                PromotionType = PromotionType.Featured,
                PromotionStartDate = DateTime.UtcNow.AddDays(-1),
                PromotionEndDate = DateTime.UtcNow.AddDays(6)
            },
            // Furniture - Sofa
            new Listing
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Title = "IKEA Sectional Sofa - Gray",
                Description = "Comfortable L-shaped sectional sofa in excellent condition. Pet-free, smoke-free home. Easy to clean fabric.",
                Price = 650,
                Status = ListingStatus.Active,
                PrimaryImageUrl = "https://example.com/images/sofa-gray-1.jpg",
                ImagesUrl = new List<string> { "https://example.com/images/sofa-gray-2.jpg" },
                Name = "John Doe",
                PhoneNumber = "+1-555-0105",
                IsPriceNegotiable = true,
                ContactMethod = ContactMethod.PhoneNumber,
                OwnerId = "user-1-id",
                CategoryId = CategorySeed.Data[4].Id,
                SubcategoryId = SubCategorySeed.Data[32].Id,
                PickupLocationId = PickupLocationSeed.Data[1].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            },
            // Phone - iPhone
            new Listing
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                Title = "iPhone 15 Pro Max 256GB - Titanium Blue",
                Description = "Brand new iPhone 15 Pro Max, unopened box. 256GB storage in Titanium Blue. Full Apple warranty.",
                Price = 1200,
                Status = ListingStatus.Sold,
                PrimaryImageUrl = "https://example.com/images/iphone-15-pro-1.jpg",
                ImagesUrl = new List<string> { "https://example.com/images/iphone-15-pro-2.jpg" },
                Name = "Bourhan Hassoun",
                PhoneNumber = "+1-555-0106",
                IsPriceNegotiable = false,
                ContactMethod = ContactMethod.InAppChat,
                OwnerId = "user-2-id",
                CategoryId = CategorySeed.Data[2].Id,
                SubcategoryId = SubCategorySeed.Data[16].Id,
                PickupLocationId = PickupLocationSeed.Data[3].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-20),
                UpdatedAt = DateTime.UtcNow.AddDays(-10),
                DeletedAt = DateTime.UtcNow.AddDays(-10)
            },
            // Services - Home Repair
            new Listing
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                Title = "Professional Plumbing Services",
                Description = "Licensed plumber with 15+ years experience. Emergency services available. Free estimates for all jobs.",
                Price = 0,
                Status = ListingStatus.Active,
                PrimaryImageUrl = "https://example.com/images/plumbing-service-1.jpg",
                ImagesUrl = new List<string>(),
                Name = "John Doe",
                PhoneNumber = "+1-555-0107",
                IsPriceNegotiable = true,
                ContactMethod = ContactMethod.PhoneNumber,
                OwnerId = "user-1-id",
                CategoryId = CategorySeed.Data[10].Id,
                SubcategoryId = SubCategorySeed.Data[47].Id,
                PickupLocationId = PickupLocationSeed.Data[0].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            },
            // Pets - Dog
            new Listing
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                Title = "Golden Retriever Puppies",
                Description = "Adorable Golden Retriever puppies looking for loving homes. Vaccinated, microchipped, and health checked. Parents on premises.",
                Price = 1500,
                Status = ListingStatus.Active,
                PrimaryImageUrl = "https://example.com/images/golden-retriever-1.jpg",
                ImagesUrl = new List<string> { "https://example.com/images/golden-retriever-2.jpg", "https://example.com/images/golden-retriever-3.jpg" },
                Name = "Bourhan Hassoun",
                PhoneNumber = "+1-555-0108",
                IsPriceNegotiable = false,
                ContactMethod = ContactMethod.PhoneNumber,
                OwnerId = "user-2-id",
                CategoryId = CategorySeed.Data[5].Id,
                SubcategoryId = SubCategorySeed.Data[36].Id,
                PickupLocationId = PickupLocationSeed.Data[2].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            }
        };
    }
}
