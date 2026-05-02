using Arzly.Api.Domain.ListingOwned;
using Arzly.Shared.Enums.ListingOwned.ServicesDetails;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class ServicesDetailsSeed
    {
        public static readonly List<ServicesDetails> Data = new()
        {
            new ServicesDetails
            {
                ListingId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                ServiceType = ServiceType.Offering,
                HomeServiceType = HomeServiceType.Plumbing,
            },
        };
    }
}
