using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class SubCategorySeed
    {
        public static readonly List<SubCategory> Data = new()
        {
            new SubCategory { Id = Guid.Parse("81b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), CategoryId = Guid.Parse("e1b1a2c8-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), Name = "Mobile Phones", Description = "Smartphones and accessories" },
            new SubCategory { Id = Guid.Parse("82b2a2c4-5d4e-4f3a-9b8c-7d6e5f4a3b2c"), CategoryId = Guid.Parse("e2b2a2c9-5d4e-4f3a-9b8c-7d6e5f4a3b2c"), Name = "Cars", Description = "Sedans, SUVs and more" },
            new SubCategory { Id = Guid.Parse("83b3a2c5-6f5e-4f2a-8b7c-9d0e1f2a3b4c"), CategoryId = Guid.Parse("e3b3a2ca-6f5e-4f2a-8b7c-9d0e1f2a3b4c"), Name = "Apartments", Description = "Rent or buy apartments" }
        };
    }
}
