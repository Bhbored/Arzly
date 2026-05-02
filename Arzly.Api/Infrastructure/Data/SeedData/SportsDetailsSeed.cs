using Arzly.Api.Domain.ListingOwned;
using Arzly.Shared.Enums.ListingOwned.SportAndOutdoors;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class SportsDetailsSeed
    {
        public static readonly List<SportsDetails> Data = new()
        {
            new SportsDetails
            {
                ListingId = Guid.Parse("10000000-0000-0000-0000-000000000007"),
                BicycleType = BicycleType.Bicycles,
                Condition = SportsCondition.Used,
                BicyclePowerType = BicyclePowerType.Manual,
            },
            new SportsDetails
            {
                ListingId = Guid.Parse("10000000-0000-0000-0000-000000000008"),
                GymType = GymType.Bodybuilding,
                Condition = SportsCondition.New,
            },
        };
    }
}
