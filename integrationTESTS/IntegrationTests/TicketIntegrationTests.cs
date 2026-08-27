using System.Net;
using System.Net.Http.Json;
using Arzly.Api.Domain.Entities.Support;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Shared.DTOs.Request.Ticket;
using Arzly.Shared.DTOs.Request.TicketAttachment;
using Arzly.Shared.DTOs.Request.TicketMessage;
using Arzly.Shared.Enums.Ticket;
using Arzly.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Arzly.IntegrationTests;

public class TicketIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private static readonly Guid OtherUserId = Guid.Parse("20000000-0000-0000-0000-000000000002");
    private static readonly Guid SupportUserId = Guid.Parse("30000000-0000-0000-0000-000000000003");
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public TicketIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _factory.ResetDatabase();
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Create_UsesAuthenticatedOwnerAndMineOnlyReturnsOwnedTickets()
    {
        await SeedUsers();
        var create = await _client.PostAsJsonAsync(
            "/arzly/v1.0/Ticket",
            new TicketAddRequest
            {
                Subject = "Need assistance",
                Priority = TicketPriority.Medium,
                UserId = OtherUserId
            });
        using (var seedScope = _factory.Services.CreateScope())
        {
            var seedDb = seedScope.ServiceProvider.GetRequiredService<AppDbContext>();
            seedDb.Tickets.Add(new Ticket
            {
                Id = Guid.NewGuid(), UserId = OtherUserId, Subject = "Other ticket",
                Priority = TicketPriority.Low, Status = TicketStatus.Open
            });
            await seedDb.SaveChangesAsync();
        }

        var mine = await _client.GetAsync("/arzly/v1.0/Ticket/mine");

        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        Assert.Equal(HttpStatusCode.OK, mine.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Assert.Single(await db.Tickets.Where(x => x.UserId == TestAuthHandler.DefaultUserId).ToListAsync());
    }

    [Fact]
    public async Task Conversation_IsOwnerOrStaffOnlyAndQueueIsStaffOnly()
    {
        var ticket = await SeedTicket(TestAuthHandler.DefaultUserId);

        var owner = await _client.GetAsync($"/arzly/v1.0/Ticket/{ticket.Id}");
        var outsider = await SendAs(HttpMethod.Get, $"/arzly/v1.0/Ticket/{ticket.Id}", "user", OtherUserId);
        var support = await SendAs(HttpMethod.Get, $"/arzly/v1.0/Ticket/{ticket.Id}", "support", SupportUserId);
        var userQueue = await _client.GetAsync("/arzly/v1.0/Ticket/queue");
        var supportQueue = await SendAs(HttpMethod.Get, "/arzly/v1.0/Ticket/queue", "support", SupportUserId);

        Assert.Equal(HttpStatusCode.OK, owner.StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, outsider.StatusCode);
        Assert.Equal(HttpStatusCode.OK, support.StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, userQueue.StatusCode);
        Assert.Equal(HttpStatusCode.OK, supportQueue.StatusCode);
    }

    [Fact]
    public async Task SupportStatusChange_AssignsTicketAndEnablesOwnerReply()
    {
        var ticket = await SeedTicket(TestAuthHandler.DefaultUserId);
        var status = await SendAs(
            HttpMethod.Put,
            $"/arzly/v1.0/Ticket/{ticket.Id}/status",
            "support",
            SupportUserId,
            JsonContent.Create(TicketStatus.InProgress));
        var reply = await _client.PostAsJsonAsync(
            $"/arzly/v1.0/Ticket/{ticket.Id}/messages",
            new TicketMessageAddRequest
            {
                TicketId = ticket.Id,
                SenderId = OtherUserId,
                ReceiverId = SupportUserId,
                Message = "Here are more details",
                IsInternalNote = true
            });

        Assert.Equal(HttpStatusCode.OK, status.StatusCode);
        Assert.Equal(HttpStatusCode.Created, reply.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var message = await db.TicketMessages.SingleAsync();
        Assert.Equal(TestAuthHandler.DefaultUserId, message.SenderId);
        Assert.Equal(SupportUserId, message.ReceiverId);
        Assert.False(message.IsInternalNote);
    }

    [Fact]
    public async Task Attachment_RequiresTicketAccessAndValidMetadata()
    {
        var ticket = await SeedTicket(TestAuthHandler.DefaultUserId);
        var validRequest = new TicketAttachmentAddRequest
        {
            TicketId = ticket.Id,
            FileUrl = "https://uploads.arzly.test/evidence.pdf",
            FileName = "evidence.pdf",
            ContentType = "application/pdf",
            FileSize = 1024
        };

        var outsider = await SendAs(
            HttpMethod.Post,
            $"/arzly/v1.0/Ticket/{ticket.Id}/attachments",
            "user",
            OtherUserId,
            JsonContent.Create(validRequest));
        validRequest.FileSize = 11 * 1024 * 1024;
        var oversized = await _client.PostAsJsonAsync(
            $"/arzly/v1.0/Ticket/{ticket.Id}/attachments",
            validRequest);
        validRequest.FileSize = 1024;
        var valid = await _client.PostAsJsonAsync(
            $"/arzly/v1.0/Ticket/{ticket.Id}/attachments",
            validRequest);

        Assert.Equal(HttpStatusCode.Unauthorized, outsider.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, oversized.StatusCode);
        Assert.Equal(HttpStatusCode.Created, valid.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var attachment = await db.TicketAttachments.SingleAsync();
        Assert.Equal(TestAuthHandler.DefaultUserId, attachment.UploaderId);
    }

    private async Task SeedUsers()
    {
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, TestAuthHandler.DefaultUserId);
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, OtherUserId);
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, SupportUserId);
    }

    private async Task<Ticket> SeedTicket(Guid ownerId)
    {
        await SeedUsers();
        var ticket = new Ticket
        {
            Id = Guid.NewGuid(), UserId = ownerId, Subject = "Integration support ticket",
            Priority = TicketPriority.Medium, Status = TicketStatus.Open
        };
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Tickets.Add(ticket);
        await db.SaveChangesAsync();
        return ticket;
    }

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
