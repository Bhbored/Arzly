using Arzly.Api.Domain.ListingOwned;
using Arzly.Shared.Enums.ListingOwned.PhonesAndGadgets;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class PhonesDetailsSeed
    {
        public static readonly List<PhonesDetails> Data = new()
        {
            new PhonesDetails
            {
                ListingId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                PhoneBrand = PhoneBrand.Apple,
                PhoneCondition = PhoneCondition.New,
                Storage = PhoneStorage.GB256,
                Color = PhoneColor.Blue,
            },
        };
    }
}
