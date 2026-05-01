using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class SearchQuerySeed
    {
        public static readonly List<SearchQuery> Data = new()
        {
            new SearchQuery
            {
                Id = Guid.NewGuid(),
                UserId = "user-1-id",
                Query = "BMW cars for sale",
                SearchedAt = DateTime.UtcNow.AddDays(-5)
            },
            new SearchQuery
            {
                Id = Guid.NewGuid(),
                UserId = "user-1-id",
                Query = "apartment for rent in Brooklyn",
                SearchedAt = DateTime.UtcNow.AddDays(-3)
            },
            new SearchQuery
            {
                Id = Guid.NewGuid(),
                UserId = "user-1-id",
                Query = "iPhone 15 Pro Max",
                SearchedAt = DateTime.UtcNow.AddDays(-1)
            },
            new SearchQuery
            {
                Id = Guid.NewGuid(),
                UserId = "user-2-id",
                Query = "gaming laptop",
                SearchedAt = DateTime.UtcNow.AddDays(-7)
            },
            new SearchQuery
            {
                Id = Guid.NewGuid(),
                UserId = "user-2-id",
                Query = "sofa set",
                SearchedAt = DateTime.UtcNow.AddDays(-2)
            }
        };
    }
}
