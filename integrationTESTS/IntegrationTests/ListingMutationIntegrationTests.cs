using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;
using Arzly.Shared.Enums.JobListing;
using Arzly.Shared.Enums.Listing;
using Arzly.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Arzly.IntegrationTests;

public class ListingMutationIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public ListingMutationIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _factory.ResetDatabase();
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Create_PersistsListingForAuthenticatedOwner()
    {
        var location = await TestDataSeeder.SeedUserWithPickupLocation(
            _factory,
            TestAuthHandler.DefaultUserId);
        var request = CreateAddRequest(location.Id);

        var response = await _client.PostAsJsonAsync("/arzly/v1.0/Listing/Create", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var created = await response.Content.ReadFromJsonAsync<ListingResponse>(JsonOptions);
        Assert.NotNull(created);
        Assert.Equal(TestAuthHandler.DefaultUserId, created.OwnerId);

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var listing = await db.Listings.SingleAsync(x => x.Id == created.Id);
        Assert.Equal(request.Title, listing.Title);
        var details = await db.VehiclesDetails.SingleAsync(x => x.ListingId == created.Id);
        Assert.Equal("Test Brand", details.CarBrand);
    }

    [Fact]
    public async Task Create_WithoutCategoryDetails_ReturnsBadRequestWithoutPersistingListing()
    {
        var location = await TestDataSeeder.SeedUserWithPickupLocation(
            _factory,
            TestAuthHandler.DefaultUserId);
        var request = CreateAddRequest(location.Id);
        request.ListingDetails = null;

        var response = await _client.PostAsJsonAsync("/arzly/v1.0/Listing/Create", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Assert.Empty(await db.Listings.ToListAsync());
    }

    [Fact]
    public async Task Create_WithAnotherUsersPickupLocation_ReturnsUnauthorized()
    {
        var otherUserId = Guid.Parse("20000000-0000-0000-0000-000000000002");
        var location = await TestDataSeeder.SeedUserWithPickupLocation(_factory, otherUserId);
        var request = CreateAddRequest(location.Id);

        var response = await _client.PostAsJsonAsync("/arzly/v1.0/Listing/Create", request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Update_ByOwner_ModifiesListing()
    {
        var (listing, location) = await SeedOwnedListing();
        var request = CreateUpdateRequest(listing.Id, location.Id, "Updated title");

        var response = await _client.PutAsJsonAsync("/arzly/v1.0/Listing/Update", request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var updated = await db.Listings.SingleAsync(x => x.Id == listing.Id);
        Assert.Equal("Updated title", updated.Title);
        Assert.NotNull(updated.UpdatedAt);
    }

    [Fact]
    public async Task Update_ByDifferentUser_ReturnsUnauthorizedWithoutChangingListing()
    {
        var (listing, location) = await SeedOwnedListing();
        var requestBody = CreateUpdateRequest(listing.Id, location.Id, "Unauthorized update");
        using var request = new HttpRequestMessage(HttpMethod.Put, "/arzly/v1.0/Listing/Update")
        {
            Content = JsonContent.Create(requestBody)
        };
        request.Headers.Add(
            TestAuthHandler.UserIdHeader,
            "20000000-0000-0000-0000-000000000002");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var unchanged = await db.Listings.SingleAsync(x => x.Id == listing.Id);
        Assert.Equal(listing.Title, unchanged.Title);
    }

    [Fact]
    public async Task Delete_ByOwner_SoftDeletesAndHidesListing()
    {
        var (listing, _) = await SeedOwnedListing();

        var response = await _client.DeleteAsync($"/arzly/v1.0/Listing/Delete/{listing.Id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Assert.False(await db.Listings.AnyAsync(x => x.Id == listing.Id));
        var deleted = await db.Listings.IgnoreQueryFilters().SingleAsync(x => x.Id == listing.Id);
        Assert.True(deleted.IsDeleted);
        Assert.NotNull(deleted.DeletedAt);
    }

    [Fact]
    public async Task Delete_ByDifferentUser_ReturnsUnauthorizedWithoutDeletingListing()
    {
        var (listing, _) = await SeedOwnedListing();
        using var request = new HttpRequestMessage(
            HttpMethod.Delete,
            $"/arzly/v1.0/Listing/Delete/{listing.Id}");
        request.Headers.Add(
            TestAuthHandler.UserIdHeader,
            "20000000-0000-0000-0000-000000000002");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Assert.True(await db.Listings.AnyAsync(x => x.Id == listing.Id));
    }

    private static ListingAddRequest CreateAddRequest(Guid pickupLocationId)
    {
        return new ListingAddRequest
        {
            Title = "Test vehicle",
            Description = "A vehicle created by an integration test",
            Price = 12000,
            CategoryId = TestDataSeeder.VehiclesCategoryId,
            SubcategoryId = TestDataSeeder.CarsSubcategoryId,
            PickupLocationId = pickupLocationId,
            Name = "Test Seller",
            PhoneNumber = "+961000000",
            ContactMethod = ContactMethod.Both,
            ListingDetails = JsonSerializer.SerializeToElement(new { CarBrand = "Test Brand" })
        };
    }

    private static ListingUpdateRequest CreateUpdateRequest(
        Guid listingId,
        Guid pickupLocationId,
        string title)
    {
        return new ListingUpdateRequest
        {
            Id = listingId,
            Title = title,
            Description = "Updated integration-test description",
            Price = 12500,
            CategoryId = TestDataSeeder.VehiclesCategoryId,
            SubcategoryId = TestDataSeeder.CarsSubcategoryId,
            PickupLocationId = pickupLocationId,
            Name = "Test Seller",
            PhoneNumber = "+961000000",
            ContactMethod = ContactMethod.Both,
            ListingDetails = JsonSerializer.SerializeToElement(new { CarBrand = "Updated Brand" })
        };
    }

    private async Task<(Listing Listing, PickupLocation Location)> SeedOwnedListing()
    {
        var location = await TestDataSeeder.SeedUserWithPickupLocation(
            _factory,
            TestAuthHandler.DefaultUserId);
        var listing = new Listing
        {
            Id = Guid.NewGuid(),
            Title = "Owner listing",
            Description = "Listing used for mutation tests",
            Price = 100,
            OwnerId = TestAuthHandler.DefaultUserId,
            CategoryId = TestDataSeeder.VehiclesCategoryId,
            SubcategoryId = TestDataSeeder.CarsSubcategoryId,
            PickupLocationId = location.Id,
            Name = "Test Seller",
            PhoneNumber = "+961000000",
            ContactMethod = ContactMethod.Both
        };

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Listings.Add(listing);
        await db.SaveChangesAsync();
        return (listing, location);
    }
}
