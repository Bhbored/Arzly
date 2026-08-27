using System.Net;
using System.Net.Http.Json;
using Arzly.Api.Domain.Entities.Communications;
using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Shared.DTOs.Request.Chat;
using Arzly.Shared.Enums.JobListing;
using Arzly.Shared.Enums.Listing;
using Arzly.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Arzly.IntegrationTests;

public class ResourceOwnershipIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private static readonly Guid OtherUserId =
        Guid.Parse("20000000-0000-0000-0000-000000000002");
    private static readonly Guid OutsiderId =
        Guid.Parse("30000000-0000-0000-0000-000000000003");

    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public ResourceOwnershipIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _factory.ResetDatabase();
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task SavedListing_CannotBeReadOrDeletedByAnotherUser()
    {
        var savedListing = await SeedSavedListing();

        var get = await SendAsOtherUser(
            HttpMethod.Get,
            $"/arzly/v1.0/SavedListing/{savedListing.Id}");
        var delete = await SendAsOtherUser(
            HttpMethod.Delete,
            $"/arzly/v1.0/SavedListing/{savedListing.Id}");

        Assert.Equal(HttpStatusCode.Unauthorized, get.StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, delete.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Assert.True(await db.SavedListings.AnyAsync(x => x.Id == savedListing.Id));
    }

    [Fact]
    public async Task PickupLocation_GetAllIsForbiddenForOrdinaryUser()
    {
        var response = await _client.GetAsync("/arzly/v1.0/PickupLocation");

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task PickupLocation_CannotBeReadOrDeletedByAnotherUser()
    {
        var location = await TestDataSeeder.SeedUserWithPickupLocation(_factory, OtherUserId);

        var get = await _client.GetAsync($"/arzly/v1.0/PickupLocation/{location.Id}");
        var delete = await _client.DeleteAsync($"/arzly/v1.0/PickupLocation/Delete/{location.Id}");

        Assert.Equal(HttpStatusCode.Unauthorized, get.StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, delete.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Assert.True(await db.PickupLocations.AnyAsync(x => x.Id == location.Id));
    }

    [Fact]
    public async Task Chat_CannotBeReadMutatedOrMessagedByOutsider()
    {
        var chat = await SeedChat();

        var get = await SendAsUser(HttpMethod.Get, $"/arzly/v1.0/Chat/{chat.Id}", OutsiderId);
        var archive = await SendAsUser(HttpMethod.Put, $"/arzly/v1.0/Chat/ToggleArchive/{chat.Id}", OutsiderId);
        var delete = await SendAsUser(HttpMethod.Delete, $"/arzly/v1.0/Chat/{chat.Id}", OutsiderId);
        var send = await SendAsUser(
            HttpMethod.Post,
            "/arzly/v1.0/Chat/SendMessage",
            OutsiderId,
            JsonContent.Create(new SendMessageRequest { ChatId = chat.Id, Text = "not allowed" }));

        Assert.Equal(HttpStatusCode.Unauthorized, get.StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, archive.StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, delete.StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, send.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var unchanged = await db.Chats.SingleAsync(x => x.Id == chat.Id);
        Assert.False(unchanged.IsArchived);
        Assert.False(unchanged.IsDeleted);
        Assert.Empty(await db.ChatMessages.ToListAsync());
    }

    [Fact]
    public async Task Chat_ParticipantCanReadAndSendMessage()
    {
        var chat = await SeedChat();

        var get = await _client.GetAsync($"/arzly/v1.0/Chat/{chat.Id}");
        var send = await _client.PostAsJsonAsync(
            "/arzly/v1.0/Chat/SendMessage",
            new SendMessageRequest { ChatId = chat.Id, Text = "hello" });

        Assert.Equal(HttpStatusCode.OK, get.StatusCode);
        Assert.Equal(HttpStatusCode.Created, send.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var message = await db.ChatMessages.SingleAsync();
        Assert.Equal(TestAuthHandler.DefaultUserId, message.SenderId);
        Assert.Equal(OtherUserId, message.ReceiverId);
    }

    [Fact]
    public async Task ChatMessage_CanOnlyBeMarkedReadByReceiver()
    {
        var chat = await SeedChat();
        var message = await SeedMessage(chat);

        var senderAttempt = await _client.PutAsJsonAsync(
            "/arzly/v1.0/Chat/MarkMessageAsRead",
            new { MessageId = message.Id });
        var receiverAttempt = await SendAsUser(
            HttpMethod.Put,
            "/arzly/v1.0/Chat/MarkMessageAsRead",
            OtherUserId,
            JsonContent.Create(new { MessageId = message.Id }));

        Assert.Equal(HttpStatusCode.Unauthorized, senderAttempt.StatusCode);
        Assert.Equal(HttpStatusCode.NoContent, receiverAttempt.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var updated = await db.ChatMessages.SingleAsync(x => x.Id == message.Id);
        Assert.True(updated.IsRead);
        Assert.NotNull(updated.ReadAt);
    }

    [Fact]
    public async Task DeletedListing_HidesItsChatAndMessagesFromNormalQueries()
    {
        var (chat, listing) = await SeedListingChat();
        var message = await SeedMessage(chat);
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var storedListing = await db.Listings.SingleAsync(x => x.Id == listing.Id);
            storedListing.IsDeleted = true;
            storedListing.DeletedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
        }

        var response = await _client.GetAsync($"/arzly/v1.0/Chat/{chat.Id}");
        var readAttempt = await SendAsUser(
            HttpMethod.Put,
            "/arzly/v1.0/Chat/MarkMessageAsRead",
            OtherUserId,
            JsonContent.Create(new { MessageId = message.Id }));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, readAttempt.StatusCode);
    }

    private async Task<SavedListing> SeedSavedListing()
    {
        var ownerLocation = await TestDataSeeder.SeedUserWithPickupLocation(
            _factory,
            TestAuthHandler.DefaultUserId);
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, OtherUserId);
        var listing = new Listing
        {
            Id = Guid.NewGuid(), Title = "Saved listing", Description = "Ownership test", Price = 10,
            OwnerId = TestAuthHandler.DefaultUserId, CategoryId = TestDataSeeder.VehiclesCategoryId,
            SubcategoryId = TestDataSeeder.CarsSubcategoryId, PickupLocationId = ownerLocation.Id,
            Name = "Owner", PhoneNumber = "+961000000", ContactMethod = ContactMethod.Both
        };
        var saved = new SavedListing
        {
            Id = Guid.NewGuid(), UserId = TestAuthHandler.DefaultUserId, ListingId = listing.Id
        };
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Listings.Add(listing);
        db.SavedListings.Add(saved);
        await db.SaveChangesAsync();
        return saved;
    }

    private async Task<Chat> SeedChat()
    {
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, TestAuthHandler.DefaultUserId);
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, OtherUserId);
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, OutsiderId);
        var chat = new Chat
        {
            Id = Guid.NewGuid(), InitiatorId = TestAuthHandler.DefaultUserId,
            ReceiverId = OtherUserId, PersonName = "Other user"
        };
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Chats.Add(chat);
        await db.SaveChangesAsync();
        return chat;
    }

    private async Task<ChatMessage> SeedMessage(Chat chat)
    {
        var message = new ChatMessage
        {
            Id = Guid.NewGuid(), ChatId = chat.Id, SenderId = TestAuthHandler.DefaultUserId,
            ReceiverId = OtherUserId, Text = "message"
        };
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.ChatMessages.Add(message);
        await db.SaveChangesAsync();
        return message;
    }

    private async Task<(Chat Chat, Listing Listing)> SeedListingChat()
    {
        var location = await TestDataSeeder.SeedUserWithPickupLocation(_factory, TestAuthHandler.DefaultUserId);
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, OtherUserId);
        var listing = new Listing
        {
            Id = Guid.NewGuid(), Title = "Chat listing", Description = "Visibility test", Price = 10,
            OwnerId = OtherUserId, CategoryId = TestDataSeeder.VehiclesCategoryId,
            SubcategoryId = TestDataSeeder.CarsSubcategoryId, PickupLocationId = location.Id,
            Name = "Seller", PhoneNumber = "+961000000", ContactMethod = ContactMethod.Both
        };
        var chat = new Chat
        {
            Id = Guid.NewGuid(), InitiatorId = TestAuthHandler.DefaultUserId,
            ReceiverId = OtherUserId, ListingId = listing.Id, PersonName = "Other user"
        };
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Listings.Add(listing);
        db.Chats.Add(chat);
        await db.SaveChangesAsync();
        return (chat, listing);
    }

    private Task<HttpResponseMessage> SendAsOtherUser(HttpMethod method, string path) =>
        SendAsUser(method, path, OtherUserId);

    private async Task<HttpResponseMessage> SendAsUser(
        HttpMethod method,
        string path,
        Guid userId,
        HttpContent? content = null)
    {
        using var request = new HttpRequestMessage(method, path) { Content = content };
        request.Headers.Add(TestAuthHandler.UserIdHeader, userId.ToString());
        return await _client.SendAsync(request);
    }
}
