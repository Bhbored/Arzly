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
                Id = Guid.Parse("91b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), 
                UserId = "7c9e6679-7425-40de-944b-e07fc1f90ae7", 
                Address = "123 Main St, New York", 
                Label = LocationLabel.Home, 
                IsDefault = true,
                Lat = 40.7128,
                Lon = -74.0060
            },
            new PickupLocation 
            { 
                Id = Guid.Parse("92b2a2c4-5d4e-4f3a-9b8c-7d6e5f4a3b2c"), 
                UserId = "8f3b2a1c-5d4e-4f3a-9b8c-7d6e5f4a3b2c", 
                Address = "456 Side St, Los Angeles", 
                Label = LocationLabel.Work, 
                IsDefault = true,
                Lat = 34.0522,
                Lon = -118.2437
            }
        };
    }
}
