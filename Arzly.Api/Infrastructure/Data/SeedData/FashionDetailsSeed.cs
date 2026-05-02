using Arzly.Api.Domain.ListingOwned;
using Arzly.Shared.Enums.ListingOwned.StyleAndWellness;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class FashionDetailsSeed
    {
        public static readonly List<FashionDetails> Data = new()
        {
            new FashionDetails
            {
                ListingId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                MensClothingType = MensClothingType.Shirts,
                Condition = FashionCondition.New,
            },
            new FashionDetails
            {
                ListingId = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                WomensClothingType = WomensClothingType.Dresses,
                Condition = FashionCondition.Used,
            },
        };
    }
}
