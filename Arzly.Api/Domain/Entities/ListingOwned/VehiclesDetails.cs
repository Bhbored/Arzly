using Arzly.Shared.Enums.VehiclesDetails;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Domain.Entities.ListingOwned
{
    [Owned]
    public class VehiclesDetails
    {
        public CarBrand? CarBrand { get; set; }
        public string? Version { get; set; }  // e.g., "XLE", "Sport", "GT"
        public VehicleCondition? Condition { get; set; }
        public int? Kilometers { get; set; }
        public int? Year { get; set; }

     
        public AccessoryType? AccessoryType { get; set; }
        public VehicleType? VehicleType { get; set; }
       
        public PlateDigits? NumberOfDigits { get; set; }

        
        public TruckBrand? TruckBrand { get; set; }
        
        public BoatType? BoatType { get; set; }
    }
}
