using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.ListingOwned.Motors;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.ListingOwned
{
    public class VehiclesDetails
    {
        [Key, ForeignKey(nameof(Listing))]
        public Guid ListingId { get; set; }
        public virtual Listing? Listing { get; set; }

        public CarBrand? CarBrand { get; set; }
        public string? Version { get; set; }
        public VehicleCondition? Condition { get; set; }
        public int? Kilometers { get; set; }
        public int? Year { get; set; }
        public FuelType? FuelType { get; set; }
        public VehicleColor? VehicleColor { get; set; }
        public int? NumberOfDoors { get; set; }
        public TransmissionType? TransmissionType { get; set; }
        public List<CarFeature>? CarFeatures { get; set; }
        public AccessoryType? AccessoryType { get; set; }
        public MotorcycleType? MotorcycleType { get; set; }
        public VehicleType? VehicleType { get; set; }
        public PlateDigits? NumberOfDigits { get; set; }
        public TruckBrand? TruckBrand { get; set; }
        public BoatType? BoatType { get; set; }
    }
}
