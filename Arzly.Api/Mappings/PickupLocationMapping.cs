using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.PickupLocation;
using Arzly.Shared.DTOs.Response.PickupLocation;
using Azure.Core;

namespace Arzly.Api.Mappings
{
    public static class PickupLocationMapping
    {
        public static PickupLocationResponse ToResponse(this PickupLocation entity)
        {
            return new PickupLocationResponse
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Label = entity.Label,
                Address = entity.Address,
                Notes = entity.Notes,
                IsDefault = entity.IsDefault,
                Lon = entity.Lon,
                Lat = entity.Lat
            };
        }

        public static PickupLocation ToEntity(this PickupLocationAddRequest request)
        {
            return new PickupLocation
            {
                UserId = request.UserId,
                Label = request.Label,
                Address = request.Address,
                Notes = request.Notes,
                IsDefault = request.IsDefault,
                Lon = request.Lon,
                Lat = request.Lat
            };
        }

        public static PickupLocation ToEntity(this PickupLocationUpdateRequest request)
        {
            return new PickupLocation
            {
                Id = request.Id,
                Label = request.Label,
                Address = request.Address,
                Notes = request.Notes,
                IsDefault = request.IsDefault,
                Lon = request.Lon,
                Lat = request.Lat
            };
        }

        public static PickupLocationUpdateRequest ToUpdateRequest(this PickupLocationResponse response)
        {
            return new PickupLocationUpdateRequest
            {
                Id = response.Id,
                Label = response.Label,
                Address = response.Address,
                Notes = response.Notes,
                IsDefault = response.IsDefault,
                Lon = response.Lon,
                Lat = response.Lat
            };
        }
    }
}
