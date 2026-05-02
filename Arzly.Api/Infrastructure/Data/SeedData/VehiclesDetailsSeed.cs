using Arzly.Api.Domain.ListingOwned;
using Arzly.Shared.Enums.ListingOwned.Motors;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class VehiclesDetailsSeed
    {
        public static readonly List<VehiclesDetails> Data = new()
        {
            new VehiclesDetails
            {
                ListingId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CarBrand = CarBrand.BMW,
                Version = "320i Luxury Line",
                Condition = VehicleCondition.Used,
                Kilometers = 25000,
                Year = 2022,
                FuelType = FuelType.Gasoline,
                VehicleColor = VehicleColor.Black,
                NumberOfDoors = 4,
                TransmissionType = TransmissionType.Automatic,
                CarFeatures = new List<CarFeature> { CarFeature.LeatherSeats, CarFeature.Navigation, CarFeature.Sunroof },
            },
            new VehiclesDetails
            {
                ListingId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                CarBrand = CarBrand.Toyota,
                Version = "Camry SE",
                Condition = VehicleCondition.Used,
                Kilometers = 45000,
                Year = 2020,
                FuelType = FuelType.Gasoline,
                VehicleColor = VehicleColor.Silver,
                NumberOfDoors = 4,
                TransmissionType = TransmissionType.Automatic,
                CarFeatures = new List<CarFeature> { CarFeature.BackupCamera, CarFeature.Bluetooth },
            },
        };
    }
}
