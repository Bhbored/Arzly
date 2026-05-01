using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class SavedListingSeed
    {
        public static readonly List<SavedListing> Data = new()
        {
            new SavedListing
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                UserId = "user-1-id",
                ListingId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                SavedAt = DateTime.UtcNow.AddDays(-2)
            },
            new SavedListing
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                UserId = "user-1-id",
                ListingId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                SavedAt = DateTime.UtcNow.AddDays(-1)
            },
            new SavedListing
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                UserId = "user-2-id",
                ListingId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                SavedAt = DateTime.UtcNow.AddDays(-5)
            },
            new SavedListing
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                UserId = "user-2-id",
                ListingId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                SavedAt = DateTime.UtcNow.AddDays(-3),
                DeletedAt = DateTime.UtcNow.AddDays(-1)
            }
        };
    }
}
