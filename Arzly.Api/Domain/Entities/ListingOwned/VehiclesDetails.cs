using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.ListingOwned.Motors;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Arzly.Api.Domain.ListingOwned
{
    public class VehiclesDetails
    {
        [Key, ForeignKey(nameof(Listing))]
        public Guid ListingId { get; set; }
        [JsonIgnore]
        public virtual Listing? Listing { get; set; }

        public string? CarBrand { get; set; }
        public string? CarModel { get; set; }
        public string? Version { get; set; }
        public int? NumberOfOwners { get; set; }
        public VehicleCondition? Condition { get; set; }
        public VehicleInterior? VehicleInterior { get; set; }
        public AirConditioning? AirConditioning { get; set; }
        public int? Kilometers { get; set; }
        public int? Year { get; set; }
        public double? FuelConsumptionLPer100Km { get; set; }
        public FuelType? FuelType { get; set; }
        public VehicleColor? VehicleColor { get; set; }
        public int? HorsePower { get; set; }
        public int? NumberOfSeats { get; set; }
        public int? NumberOfDoors { get; set; }
        public CarType? CarType { get; set; }
        public TransmissionType? TransmissionType { get; set; }
        public List<CarFeature>? CarFeatures { get; set; }
        public AccessoryType? AccessoryType { get; set; }
        public MotorcycleType? MotorcycleType { get; set; }
        public PlateDigits? NumberOfDigits { get; set; }
        public TruckBrand? TruckBrand { get; set; }
        public BoatType? BoatType { get; set; }
    }
}
