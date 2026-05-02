using Arzly.Api.Domain.ListingOwned;
using Arzly.Shared.Enums.ListingOwned.BabyAndChild;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class BabyChildDetailsSeed
    {
        public static readonly List<BabyChildDetails> Data = new()
        {
            new BabyChildDetails
            {
                ListingId = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                AgeRange = KidsAgeRange.LessThan2Years,
                Condition = BabyChildCondition.New,
                Gender = KidGender.Boy,
            },
            new BabyChildDetails
            {
                ListingId = Guid.Parse("10000000-0000-0000-0000-000000000004"),
                StrollerSeatType = StrollerSeatType.Strollers,
                Condition = BabyChildCondition.Used,
            },
        };
    }
}
