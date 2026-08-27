using System.Net;
using System.Net.Http.Json;
using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Shared.Enums.JobListing;
using Arzly.Shared.Enums.Listing;
using Arzly.Shared.Enums.Activity;
using Arzly.Shared.DTOs.Response.UserActivityLog;
using Arzly.Shared.DTOs.Response.Admin;
using Arzly.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Arzly.IntegrationTests;

public class ListingAdminIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public ListingAdminIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _factory.ResetDatabase();
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData("user")]
    [InlineData("support")]
    public async Task StatusMutation_RejectsNonAdminRoles(string role)
    {
        var listing = await SeedListing();
        using var request = new HttpRequestMessage(
            HttpMethod.Put,
            $"/arzly/v1.0/admin/ListingAdmin/{listing.Id}/status")
        {
            Content = JsonContent.Create(ListingStatus.Active)
        };
        request.Headers.Add(TestAuthHandler.RoleHeader, role);

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Admin_CanApproveDeleteInspectAndRestoreListing()
    {
        var listing = await SeedListing();

        var approve = await SendAsAdmin(
            HttpMethod.Put,
            $"/arzly/v1.0/admin/ListingAdmin/{listing.Id}/status",
            JsonContent.Create(ListingStatus.Active));
        var delete = await SendAsAdmin(
            HttpMethod.Delete,
            $"/arzly/v1.0/admin/ListingAdmin/{listing.Id}");
        var publicRead = await _client.GetAsync($"/arzly/v1.0/Listing/{listing.Id}");
        var adminRead = await SendAsAdmin(
            HttpMethod.Get,
            $"/arzly/v1.0/admin/ListingAdmin/{listing.Id}");
        var restore = await SendAsAdmin(
            HttpMethod.Post,
            $"/arzly/v1.0/admin/ListingAdmin/{listing.Id}/restore");

        Assert.Equal(HttpStatusCode.OK, approve.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, delete.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, publicRead.StatusCode);
        Assert.Equal(HttpStatusCode.OK, adminRead.StatusCode);
        Assert.Equal(HttpStatusCode.OK, restore.StatusCode);

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var restored = await db.Listings.SingleAsync(x => x.Id == listing.Id);
        Assert.False(restored.IsDeleted);
        Assert.Null(restored.DeletedAt);
        Assert.Equal(ListingStatus.Pending, restored.Status);
    }

    [Fact]
    public async Task AdminList_IncludesSoftDeletedListings()
    {
        var listing = await SeedListing();
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var storedListing = await db.Listings.SingleAsync(x => x.Id == listing.Id);
            storedListing.IsDeleted = true;
            storedListing.Status = ListingStatus.Deleted;
            storedListing.DeletedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
        }

        var response = await SendAsAdmin(
            HttpMethod.Get,
            "/arzly/v1.0/admin/ListingAdmin/get-all");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadAsStringAsync();
        Assert.Contains(listing.Id.ToString(), body, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task PurgeExpired_RemovesOnlyListingsPastRetentionAndPreservesAudit()
    {
        var expired = await SeedListing();
        var recent = new Listing
        {
            Id = Guid.NewGuid(), Title = "Recent deletion", Description = "Still retained",
            Price = 25, OwnerId = expired.OwnerId, CategoryId = expired.CategoryId,
            SubcategoryId = expired.SubcategoryId, PickupLocationId = expired.PickupLocationId,
            Name = "Seller", PhoneNumber = "+961000000", ContactMethod = ContactMethod.Both
        };
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Listings.Add(recent);
            var storedExpired = await db.Listings.SingleAsync(x => x.Id == expired.Id);
            storedExpired.IsDeleted = true;
            storedExpired.Status = ListingStatus.Deleted;
            storedExpired.DeletedAt = DateTime.UtcNow.AddDays(-31);
            storedExpired.PrimaryImageUrl =
                $"https://uploads.arzly.test/{storedExpired.OwnerId}/{Guid.NewGuid():N}.png";
            recent.IsDeleted = true;
            recent.Status = ListingStatus.Deleted;
            recent.DeletedAt = DateTime.UtcNow.AddDays(-29);
            await db.SaveChangesAsync();
        }

        var preview = await SendAsAdmin(
            HttpMethod.Get, "/arzly/v1.0/admin/ListingAdmin/purge-preview");
        var purge = await SendAsAdmin(
            HttpMethod.Post, "/arzly/v1.0/admin/ListingAdmin/purge-expired?batchSize=10");

        Assert.Equal(HttpStatusCode.OK, preview.StatusCode);
        Assert.Contains("\"eligibleListings\":1", await preview.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, purge.StatusCode);
        var result = await purge.Content.ReadFromJsonAsync<ListingPurgeResultResponse>();
        Assert.NotNull(result);
        Assert.Equal(1, result.PurgedListings);
        Assert.Equal(1, result.DeletedImages);

        using var verificationScope = _factory.Services.CreateScope();
        var verificationDb = verificationScope.ServiceProvider.GetRequiredService<AppDbContext>();
        Assert.False(await verificationDb.Listings.IgnoreQueryFilters().AnyAsync(x => x.Id == expired.Id));
        Assert.True(await verificationDb.Listings.IgnoreQueryFilters().AnyAsync(x => x.Id == recent.Id));
        Assert.True(await verificationDb.UserActivityLogs.AnyAsync(x =>
            x.TargetId == expired.Id.ToString() && x.ActionType == ActivityActionType.ListingPurged));
    }

    [Fact]
    public async Task PurgeExpired_RejectsNonAdminUsers()
    {
        using var request = new HttpRequestMessage(
            HttpMethod.Post, "/arzly/v1.0/admin/ListingAdmin/purge-expired");
        request.Headers.Add(TestAuthHandler.RoleHeader, "support");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task RejectionReason_IsPersistedClearedOnApprovalAndAudited()
    {
        var listing = await SeedListing();
        var reject = await SendAsAdmin(
            HttpMethod.Put,
            $"/arzly/v1.0/admin/ListingAdmin/{listing.Id}/reject",
            JsonContent.Create(new { Reason = "Missing ownership evidence" }));

        Assert.Equal(HttpStatusCode.OK, reject.StatusCode);
        using (var rejectedScope = _factory.Services.CreateScope())
        {
            var rejectedDb = rejectedScope.ServiceProvider.GetRequiredService<AppDbContext>();
            var rejected = await rejectedDb.Listings.SingleAsync(x => x.Id == listing.Id);
            Assert.Equal(ListingStatus.Rejected, rejected.Status);
            Assert.Equal("Missing ownership evidence", rejected.RejectionReason);
        }

        var approve = await SendAsAdmin(
            HttpMethod.Put,
            $"/arzly/v1.0/admin/ListingAdmin/{listing.Id}/status",
            JsonContent.Create(ListingStatus.Active));
        var history = await SendAsAdmin(
            HttpMethod.Get,
            $"/arzly/v1.0/admin/ListingAdmin/{listing.Id}/history");

        Assert.Equal(HttpStatusCode.OK, approve.StatusCode);
        Assert.Equal(HttpStatusCode.OK, history.StatusCode);
        var auditEntries = await history.Content.ReadFromJsonAsync<List<UserActivityLogResponse>>();
        Assert.NotNull(auditEntries);
        Assert.Contains(auditEntries, x => x.ActionType == ActivityActionType.ListingRejected);
        Assert.Contains(auditEntries, x => x.ActionType == ActivityActionType.ListingApproved);
        using var approvedScope = _factory.Services.CreateScope();
        var approvedDb = approvedScope.ServiceProvider.GetRequiredService<AppDbContext>();
        var approved = await approvedDb.Listings.SingleAsync(x => x.Id == listing.Id);
        Assert.Null(approved.RejectionReason);
        Assert.Equal(2, await approvedDb.UserActivityLogs.CountAsync(x => x.TargetId == listing.Id.ToString()));
    }

    [Fact]
    public async Task OperationalStatistics_AreAdminOnlyAndReflectStoredData()
    {
        await SeedListing();

        using var supportRequest = new HttpRequestMessage(
            HttpMethod.Get,
            "/arzly/v1.0/admin/operations/statistics");
        supportRequest.Headers.Add(TestAuthHandler.RoleHeader, "support");
        var support = await _client.SendAsync(supportRequest);
        var admin = await SendAsAdmin(HttpMethod.Get, "/arzly/v1.0/admin/operations/statistics");

        Assert.Equal(HttpStatusCode.Forbidden, support.StatusCode);
        Assert.Equal(HttpStatusCode.OK, admin.StatusCode);
        var statistics = await admin.Content.ReadFromJsonAsync<OperationalStatisticsResponse>();
        Assert.NotNull(statistics);
        Assert.Equal(1, statistics.PendingListings);
        Assert.Equal(1, statistics.Users);
    }

    private async Task<Listing> SeedListing()
    {
        var location = await TestDataSeeder.SeedUserWithPickupLocation(
            _factory,
            TestAuthHandler.DefaultUserId);
        var listing = new Listing
        {
            Id = Guid.NewGuid(), Title = "Moderation candidate", Description = "Admin test",
            Price = 50, OwnerId = TestAuthHandler.DefaultUserId,
            CategoryId = TestDataSeeder.VehiclesCategoryId,
            SubcategoryId = TestDataSeeder.CarsSubcategoryId,
            PickupLocationId = location.Id, Name = "Seller", PhoneNumber = "+961000000",
            ContactMethod = ContactMethod.Both
        };
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Listings.Add(listing);
        await db.SaveChangesAsync();
        return listing;
    }

    private async Task<HttpResponseMessage> SendAsAdmin(
        HttpMethod method,
        string path,
        HttpContent? content = null)
    {
        using var request = new HttpRequestMessage(method, path) { Content = content };
        request.Headers.Add(TestAuthHandler.RoleHeader, "admin");
        return await _client.SendAsync(request);
    }
}
