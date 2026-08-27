using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.DTOs.Response.Listing;
using Arzly.Shared.Enums;
using Arzly.Shared.Enums.JobListing;
using Arzly.Shared.Enums.Listing;
using Arzly.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Arzly.IntegrationTests;

public class ListingReadIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    private static readonly Guid VehiclesCategoryId =
        Guid.Parse("A1B2C3D4-0001-0001-0001-000000000001");

    private static readonly Guid CarsSubcategoryId =
        Guid.Parse("B1B2C3D4-0002-0002-0002-000000000001");

    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public ListingReadIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _factory.ResetDatabase();
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Indexed_ReturnsOnlyActiveNonDeletedListings()
    {
        var active = CreateListing("Active listing", ListingStatus.Active);
        var pending = CreateListing("Pending listing", ListingStatus.Pending);
        var deleted = CreateListing("Deleted listing", ListingStatus.Active);
        deleted.IsDeleted = true;
        deleted.DeletedAt = DateTime.UtcNow;
        await SeedListings(active, pending, deleted);

        var response = await _client.GetAsync("/arzly/v1.0/Listing/indexed");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var listings = await response.Content.ReadFromJsonAsync<List<ListingResponse>>(JsonOptions);
        var listing = Assert.Single(Assert.IsType<List<ListingResponse>>(listings));
        Assert.Equal(active.Id, listing.Id);
    }

    [Fact]
    public async Task Indexed_HonorsHeaderPagination()
    {
        var oldest = CreateListing(
            "Oldest",
            ListingStatus.Active,
            DateTime.UtcNow.AddMinutes(-3));
        var middle = CreateListing(
            "Middle",
            ListingStatus.Active,
            DateTime.UtcNow.AddMinutes(-2));
        var newest = CreateListing(
            "Newest",
            ListingStatus.Active,
            DateTime.UtcNow.AddMinutes(-1));
        await SeedListings(oldest, middle, newest);

        using var request = new HttpRequestMessage(HttpMethod.Get, "/arzly/v1.0/Listing/indexed");
        request.Headers.Add("pageSize", "1");
        request.Headers.Add("currentPage", "1");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var listings = await response.Content.ReadFromJsonAsync<List<ListingResponse>>(JsonOptions);
        var listing = Assert.Single(Assert.IsType<List<ListingResponse>>(listings));
        Assert.Equal(middle.Id, listing.Id);
    }

    [Fact]
    public async Task GetById_ReturnsListingWithPickupLocation()
    {
        var expected = CreateListing("Listing details", ListingStatus.Active);
        await SeedListings(expected);

        var response = await _client.GetAsync($"/arzly/v1.0/Listing/{expected.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var listing = await response.Content.ReadFromJsonAsync<ListingResponse>(JsonOptions);
        Assert.NotNull(listing);
        Assert.Equal(expected.Id, listing.Id);
        Assert.Equal(expected.Title, listing.Title);
        Assert.NotNull(listing.PickupLocation);
        Assert.Equal("Beirut test address", listing.PickupLocation.Address);
    }

    private static Listing CreateListing(
        string title,
        ListingStatus status,
        DateTime? createdAt = null)
    {
        return new Listing
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = $"Description for {title}",
            Price = 100,
            Status = status,
            CreatedAt = createdAt ?? DateTime.UtcNow,
            OwnerId = TestAuthHandler.DefaultUserId,
            CategoryId = VehiclesCategoryId,
            SubcategoryId = CarsSubcategoryId,
            Name = "Test Seller",
            PhoneNumber = "+961000000",
            ContactMethod = ContactMethod.Both
        };
    }

    private async Task SeedListings(params Listing[] listings)
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var user = new ApplicationUser
        {
            Id = TestAuthHandler.DefaultUserId,
            UserName = "testuser@arzly.test",
            NormalizedUserName = "TESTUSER@ARZLY.TEST",
            Email = "testuser@arzly.test",
            NormalizedEmail = "TESTUSER@ARZLY.TEST"
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

        foreach (var listing in listings)
        {
            listing.PickupLocationId = pickupLocation.Id;
        }

        db.Users.Add(user);
        db.PickupLocations.Add(pickupLocation);
        db.Listings.AddRange(listings);
        await db.SaveChangesAsync();
    }
}
