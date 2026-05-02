using Arzly.Api.Domain.ListingOwned;
using Arzly.Shared.Enums.ListingOwned.LeisureAndInterests;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class HobbiesDetailsSeed
    {
        public static readonly List<HobbiesDetails> Data = new()
        {
            new HobbiesDetails
            {
                ListingId = Guid.Parse("10000000-0000-0000-0000-000000000005"),
                CollectibleType = CollectibleType.Currency,
                Condition = HobbiesCondition.Used,
            },
            new HobbiesDetails
            {
                ListingId = Guid.Parse("10000000-0000-0000-0000-000000000006"),
                InstrumentType = InstrumentType.GuitarsAndStrings,
                Condition = HobbiesCondition.New,
            },
        };
    }
}
