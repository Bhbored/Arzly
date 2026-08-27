using System.Net;
using System.Net.Http.Json;
using Arzly.Api.Domain.Entities.Communications;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Shared.DTOs.Request.Notification;
using Arzly.Shared.Enums.Notification;
using Arzly.Shared.Enums.Activity;
using Arzly.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Arzly.IntegrationTests;

public class NotificationIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private static readonly Guid OtherUserId = Guid.Parse("20000000-0000-0000-0000-000000000002");
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public NotificationIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _factory.ResetDatabase();
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Inbox_OnlyReturnsCurrentUsersUnexpiredNotifications()
    {
        await SeedUsers();
        await SeedNotification(TestAuthHandler.DefaultUserId, "Mine");
        await SeedNotification(OtherUserId, "Other");
        await SeedNotification(TestAuthHandler.DefaultUserId, "Expired", DateTime.UtcNow.AddMinutes(-1));

        var response = await _client.GetAsync("/arzly/v1.0/Notification");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadAsStringAsync();
        Assert.Contains("Mine", body);
        Assert.DoesNotContain("Other", body);
        Assert.DoesNotContain("Expired", body);
    }

    [Fact]
    public async Task MarkRead_OnlyAllowsNotificationOwner()
    {
        await SeedUsers();
        var notification = await SeedNotification(TestAuthHandler.DefaultUserId, "Read me");

        var outsider = await SendAs(
            HttpMethod.Put,
            $"/arzly/v1.0/Notification/{notification.Id}/read",
            "user",
            OtherUserId);
        var owner = await _client.PutAsync($"/arzly/v1.0/Notification/{notification.Id}/read", null);

        Assert.Equal(HttpStatusCode.Unauthorized, outsider.StatusCode);
        Assert.Equal(HttpStatusCode.OK, owner.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var updated = await db.Notifications.SingleAsync(x => x.Id == notification.Id);
        Assert.True(updated.IsRead);
        Assert.NotNull(updated.ReadAt);
    }

    [Theory]
    [InlineData("user")]
    [InlineData("support")]
    public async Task DeliveryEndpoints_RejectNonAdminRoles(string role)
    {
        await SeedUsers();
        var request = CreateRequest(OtherUserId, "Restricted");

        var response = await SendAs(
            HttpMethod.Post,
            "/arzly/v1.0/admin/notifications/targeted",
            role,
            TestAuthHandler.DefaultUserId,
            JsonContent.Create(request));

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task AdminTargetedDelivery_OnlyAppearsForTargetUser()
    {
        await SeedUsers();
        var delivery = await SendAs(
            HttpMethod.Post,
            "/arzly/v1.0/admin/notifications/targeted",
            "admin",
            TestAuthHandler.DefaultUserId,
            JsonContent.Create(CreateRequest(OtherUserId, "Targeted")));
        var senderInbox = await _client.GetAsync("/arzly/v1.0/Notification");
        var targetInbox = await SendAs(HttpMethod.Get, "/arzly/v1.0/Notification", "user", OtherUserId);

        Assert.Equal(HttpStatusCode.Created, delivery.StatusCode);
        Assert.DoesNotContain("Targeted", await senderInbox.Content.ReadAsStringAsync());
        Assert.Contains("Targeted", await targetInbox.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task AdminBroadcast_CreatesIndependentUnreadNotificationForEveryUser()
    {
        await SeedUsers();
        var request = CreateRequest(null, "Maintenance");

        var response = await SendAs(
            HttpMethod.Post,
            "/arzly/v1.0/admin/notifications/broadcast",
            "admin",
            TestAuthHandler.DefaultUserId,
            JsonContent.Create(request));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var notifications = await db.Notifications.ToListAsync();
        Assert.Equal(2, notifications.Count);
        Assert.All(notifications, item =>
        {
            Assert.True(item.IsBroadcast);
            Assert.False(item.IsRead);
            Assert.NotNull(item.UserId);
        });
        var audit = await db.UserActivityLogs.SingleAsync();
        Assert.Equal(ActivityActionType.NotificationBroadcast, audit.ActionType);
        Assert.Contains("2 users", audit.Details);
    }

    private async Task SeedUsers()
    {
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, TestAuthHandler.DefaultUserId);
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, OtherUserId);
    }

    private async Task<Notification> SeedNotification(Guid userId, string title, DateTime? expiresAt = null)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(), UserId = userId, Title = title, Body = "Notification body",
            Source = NotificationSource.System, ActionType = NotificationActionType.Announcement,
            ExpiresAt = expiresAt
        };
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Notifications.Add(notification);
        await db.SaveChangesAsync();
        return notification;
    }

    private static NotificationAddRequest CreateRequest(Guid? userId, string title) => new()
    {
        UserId = userId,
        Title = title,
        Body = "Please review this announcement",
        ActionType = NotificationActionType.Announcement,
        Source = NotificationSource.Chat,
        IsBroadcast = false
    };

    private async Task<HttpResponseMessage> SendAs(
        HttpMethod method,
        string path,
        string role,
        Guid userId,
        HttpContent? content = null)
    {
        using var request = new HttpRequestMessage(method, path) { Content = content };
        request.Headers.Add(TestAuthHandler.RoleHeader, role);
        request.Headers.Add(TestAuthHandler.UserIdHeader, userId.ToString());
        return await _client.SendAsync(request);
    }
}
