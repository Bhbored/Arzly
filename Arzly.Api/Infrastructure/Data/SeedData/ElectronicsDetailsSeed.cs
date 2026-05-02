using Arzly.Api.Domain.ListingOwned;
using Arzly.Shared.Enums.ListingOwned.HomeElectronics;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class ElectronicsDetailsSeed
    {
        public static readonly List<ElectronicsDetails> Data = new()
        {
            new ElectronicsDetails
            {
                ListingId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                Condition = ElectronicsCondition.New,
                ComputerBrand = ComputerBrand.Apple,
                ComputerType = ComputerType.Laptop,
                Processor = Processor.M2,
                RamSize = RamSize.GB32,
                ComputerScreenSize = ComputerScreenSize.Inches16to16_9,
                ComputerStorage = ComputerStorage.TB1,
                StorageType = StorageType.SSD,
                ComputerColor = ComputerColor.Gray,
            },
        };
    }
}
