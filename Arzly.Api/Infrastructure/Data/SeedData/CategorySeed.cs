using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class CategorySeed
    {
        public static readonly List<Category> Data = new()
        {
            new Category { Id = Guid.Parse("e1b1a2c8-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), Name = "Electronics", Description = "Gadgets and devices", ImageUrl = "https://example.com/electronics.jpg" },
            new Category { Id = Guid.Parse("e2b2a2c9-5d4e-4f3a-9b8c-7d6e5f4a3b2c"), Name = "Vehicles", Description = "Cars, bikes and more", ImageUrl = "https://example.com/vehicles.jpg" },
            new Category { Id = Guid.Parse("e3b3a2ca-6f5e-4f2a-8b7c-9d0e1f2a3b4c"), Name = "Real Estate", Description = "Houses and apartments", ImageUrl = "https://example.com/realestate.jpg" }
        };
    }
}
