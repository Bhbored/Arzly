using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Arzly.Tests.Helpers;

public static class TestDataSeeder
{
    public static readonly Guid VehiclesCategoryId =
        Guid.Parse("A1B2C3D4-0001-0001-0001-000000000001");

    public static readonly Guid CarsSubcategoryId =
        Guid.Parse("B1B2C3D4-0002-0002-0002-000000000001");

    public static async Task<PickupLocation> SeedUserWithPickupLocation(
        CustomWebApplicationFactory factory,
        Guid userId)
    {
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userToken = userId.ToString("N");
        var user = new ApplicationUser
        {
            Id = userId,
            UserName = $"{userToken}@arzly.test",
            NormalizedUserName = $"{userToken}@ARZLY.TEST".ToUpperInvariant(),
            Email = $"{userToken}@arzly.test",
            NormalizedEmail = $"{userToken}@ARZLY.TEST".ToUpperInvariant(),
            SecurityStamp = Guid.NewGuid().ToString()
        };
        var pickupLocation = new PickupLocation
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Address = "Beirut test address",
            LocationPreset = LocationPreset.Beirut,
            Lat = 33.8938,
            Lon = 35.5018
        };

        db.Users.Add(user);
        db.PickupLocations.Add(pickupLocation);
        await db.SaveChangesAsync();
        return pickupLocation;
    }
}
