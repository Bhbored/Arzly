using Arzly.Api.Domain.ListingOwned;
using Arzly.Shared.Enums.ListingOwned.HomeAndLiving;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class FurnitureDetailsSeed
    {
        public static readonly List<FurnitureDetails> Data = new()
        {
            new FurnitureDetails
            {
                ListingId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                LivingRoomType = LivingRoomType.ChairsAndSofas,
                Condition = FurnitureCondition.Used,
            },
        };
    }
}
