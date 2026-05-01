using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.Listing;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class PickupLocationSeed
    {
        public static readonly List<PickupLocation> Data = new()
        {
            new PickupLocation
            {
                Id = Guid.NewGuid(),
                UserId = "user-1-id",
                Label = LocationLabel.Home,
                Address = "123 Main Street, New York, NY 10001",
                Notes = "Ring the doorbell twice",
                Lat = 40.7128,
                Lon = -74.0060,
                IsDefault = true
            },
            new PickupLocation
            {
                Id = Guid.NewGuid(),
                UserId = "user-1-id",
                Label = LocationLabel.Work,
                Address = "456 Business Ave, New York, NY 10002",
                Notes = "Call before arriving",
                Lat = 40.7580,
                Lon = -73.9855,
                IsDefault = false
            },
            new PickupLocation
            {
                Id = Guid.NewGuid(),
                UserId = "user-2-id",
                Label = LocationLabel.Home,
                Address = "789 Park Lane, Brooklyn, NY 11201",
                Lat = 40.6782,
                Lon = -73.9442,
                IsDefault = true
            },
            new PickupLocation
            {
                Id = Guid.NewGuid(),
                UserId = "user-2-id",
                Label = LocationLabel.Other,
                Address = "321 Shopping Mall, Queens, NY 11354",
                Notes = "Near the food court entrance",
                Lat = 40.7282,
                Lon = -73.7949,
                IsDefault = false
            }
        };
    }
}
