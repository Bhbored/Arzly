using Arzly.Api.Domain.Entities;
using Arzly.Api.Domain.ListingOwned;

namespace Arzly.Api.Helpers.ListingFilters
{
    public static class VehiclesDetailsFilter
    {
        public static IQueryable<Listing> Apply(IQueryable<Listing> query, VehiclesDetails v)
        {
            if (v.VehicleType.HasValue)
                query = query.Where(x => x.VehiclesDetails!.VehicleType == v.VehicleType);
            if (v.Condition.HasValue)
                query = query.Where(x => x.VehiclesDetails!.Condition == v.Condition);
            if (!string.IsNullOrWhiteSpace(v.CarBrand))
                query = query.Where(x => x.VehiclesDetails!.CarBrand == v.CarBrand);
            if (!string.IsNullOrWhiteSpace(v.CarModel))
                query = query.Where(x => x.VehiclesDetails!.CarModel == v.CarModel);
            if (!string.IsNullOrWhiteSpace(v.Version))
                query = query.Where(x => x.VehiclesDetails!.Version == v.Version);
            if (v.Year.HasValue)
                query = query.Where(x => x.VehiclesDetails!.Year == v.Year);
            if (v.Kilometers.HasValue)
                query = query.Where(x => x.VehiclesDetails!.Kilometers <= v.Kilometers);
            if (v.FuelType.HasValue)
                query = query.Where(x => x.VehiclesDetails!.FuelType == v.FuelType);
            if (v.FuelConsumptionLPer100Km.HasValue)
                query = query.Where(x => x.VehiclesDetails!.FuelConsumptionLPer100Km <= v.FuelConsumptionLPer100Km);
            if (v.TransmissionType.HasValue)
                query = query.Where(x => x.VehiclesDetails!.TransmissionType == v.TransmissionType);
            if (v.VehicleColor.HasValue)
                query = query.Where(x => x.VehiclesDetails!.VehicleColor == v.VehicleColor);
            if (v.HorsePower.HasValue)
                query = query.Where(x => x.VehiclesDetails!.HorsePower >= v.HorsePower);
            if (v.NumberOfSeats.HasValue)
                query = query.Where(x => x.VehiclesDetails!.NumberOfSeats == v.NumberOfSeats);
            if (v.NumberOfDoors.HasValue)
                query = query.Where(x => x.VehiclesDetails!.NumberOfDoors == v.NumberOfDoors);
            if (v.CarType.HasValue)
                query = query.Where(x => x.VehiclesDetails!.CarType == v.CarType);
            if (v.VehicleInterior.HasValue)
                query = query.Where(x => x.VehiclesDetails!.VehicleInterior == v.VehicleInterior);
            if (v.AirConditioning.HasValue)
                query = query.Where(x => x.VehiclesDetails!.AirConditioning == v.AirConditioning);
            if (v.NumberOfOwners.HasValue)
                query = query.Where(x => x.VehiclesDetails!.NumberOfOwners <= v.NumberOfOwners);
            if (v.AccessoryType.HasValue)
                query = query.Where(x => x.VehiclesDetails!.AccessoryType == v.AccessoryType);
            if (!string.IsNullOrWhiteSpace(v.MotorcycleBrand))
                query = query.Where(x => x.VehiclesDetails!.MotorcycleBrand == v.MotorcycleBrand);
            if (!string.IsNullOrWhiteSpace(v.MotorcycleModel))
                query = query.Where(x => x.VehiclesDetails!.MotorcycleModel == v.MotorcycleModel);
            if (v.MotorcycleType.HasValue)
                query = query.Where(x => x.VehiclesDetails!.MotorcycleType == v.MotorcycleType);
            if (v.MotorcycleFuelType.HasValue)
                query = query.Where(x => x.VehiclesDetails!.MotorcycleFuelType == v.MotorcycleFuelType);
            if (v.MotorcycleCC.HasValue)
                query = query.Where(x => x.VehiclesDetails!.MotorcycleCC == v.MotorcycleCC);
            if (v.NumberOfDigits.HasValue)
                query = query.Where(x => x.VehiclesDetails!.NumberOfDigits == v.NumberOfDigits);
            if (v.TruckBrand.HasValue)
                query = query.Where(x => x.VehiclesDetails!.TruckBrand == v.TruckBrand);
            if (v.BoatType.HasValue)
                query = query.Where(x => x.VehiclesDetails!.BoatType == v.BoatType);
            if (v.PartType.HasValue)
                query = query.Where(x => x.VehiclesDetails!.PartType == v.PartType);

            return query;
        }
    }
}
