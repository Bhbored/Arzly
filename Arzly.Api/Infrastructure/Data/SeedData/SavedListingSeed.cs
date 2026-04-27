using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class SavedListingSeed
    {
        public static readonly List<SavedListing> Data = new()
        {
            new SavedListing 
            { 
                Id = Guid.Parse("31b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), 
                UserId = "8f3b2a1c-5d4e-4f3a-9b8c-7d6e5f4a3b2c", 
                ListingId = Guid.Parse("11b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"),
                SavedAt = DateTime.UtcNow
            }
        };
    }
}
