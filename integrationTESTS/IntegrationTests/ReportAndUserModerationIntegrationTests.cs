using System.Net;
using System.Net.Http.Json;
using Arzly.Api.Domain.Entities.Users;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.DTOs.Request.UserModeration;
using Arzly.Shared.DTOs.Response.UserActivityLog;
using Arzly.Shared.Enums.Activity;
using Arzly.Shared.DTOs.Request.UserReport;
using Arzly.Shared.Enums;
using Arzly.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace Arzly.IntegrationTests;

public class ReportAndUserModerationIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private static readonly Guid OtherUserId = Guid.Parse("20000000-0000-0000-0000-000000000002");
    private static readonly Guid OutsiderId = Guid.Parse("30000000-0000-0000-0000-000000000003");
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public ReportAndUserModerationIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _factory.ResetDatabase();
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateReport_UsesAuthenticatedReporterInsteadOfSpoofedBodyId()
    {
        await SeedUsers();
        var response = await _client.PostAsJsonAsync(
            "/arzly/v1.0/UserReport/Create",
            new UserReportAddRequest
            {
                ReporterId = OutsiderId,
                ReportedUserId = OtherUserId,
                Reason = ReportReasonType.Fraud,
                Notes = "Suspicious behavior"
            });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var report = await db.UserReports.SingleAsync();
        Assert.Equal(TestAuthHandler.DefaultUserId, report.ReporterId);
    }

    [Fact]
    public async Task OrdinaryUser_CannotListResolveOrDeleteReports()
    {
        var report = await SeedReport();

        var list = await _client.GetAsync("/arzly/v1.0/UserReport");
        var resolve = await _client.PutAsJsonAsync(
            "/arzly/v1.0/UserReport/Update",
            new UserReportUpdateRequest { Id = report.Id, IsResolved = true });
        var delete = await _client.DeleteAsync($"/arzly/v1.0/UserReport/{report.Id}");

        Assert.Equal(HttpStatusCode.Forbidden, list.StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, resolve.StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, delete.StatusCode);
    }

    [Fact]
    public async Task Report_IsVisibleToReporterAndStaffButNotAnotherUser()
    {
        var report = await SeedReport();

        var owner = await _client.GetAsync($"/arzly/v1.0/UserReport/{report.Id}");
        var outsider = await SendAs(HttpMethod.Get, $"/arzly/v1.0/UserReport/{report.Id}", "user", OutsiderId);
        var support = await SendAs(HttpMethod.Get, $"/arzly/v1.0/UserReport/{report.Id}", "support", OutsiderId);

        Assert.Equal(HttpStatusCode.OK, owner.StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, outsider.StatusCode);
        Assert.Equal(HttpStatusCode.OK, support.StatusCode);
    }

    [Fact]
    public async Task SupportCanResolveReportButCannotBanUser()
    {
        var report = await SeedReport();
        var resolve = await SendAs(
            HttpMethod.Put,
            "/arzly/v1.0/UserReport/Update",
            "support",
            OutsiderId,
            JsonContent.Create(new UserReportUpdateRequest { Id = report.Id, IsResolved = true }));
        var ban = await SendAs(
            HttpMethod.Put,
            $"/arzly/v1.0/admin/users/{OtherUserId}/ban",
            "support",
            OutsiderId,
            JsonContent.Create(new BanUserRequest { Reason = "Not allowed" }));

        Assert.Equal(HttpStatusCode.OK, resolve.StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, ban.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var updated = await db.UserReports.SingleAsync(x => x.Id == report.Id);
        Assert.True(updated.IsResolved);
        Assert.Equal(OutsiderId, updated.ResolvedById);
    }

    [Fact]
    public async Task AdminCanBanAndUnbanUserAndBanRevokesRefreshToken()
    {
        await SeedUsers();
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var user = await db.Users.SingleAsync(x => x.Id == OtherUserId);
            user.RefreshToken = "active";
            user.RefreshTokenExpirateDate = DateTime.UtcNow.AddDays(1);
            await db.SaveChangesAsync();
        }

        var ban = await SendAs(
            HttpMethod.Put,
            $"/arzly/v1.0/admin/users/{OtherUserId}/ban",
            "admin",
            TestAuthHandler.DefaultUserId,
            JsonContent.Create(new BanUserRequest { Reason = "Fraud review" }));
        var unban = await SendAs(
            HttpMethod.Put,
            $"/arzly/v1.0/admin/users/{OtherUserId}/unban",
            "admin",
            TestAuthHandler.DefaultUserId);

        Assert.Equal(HttpStatusCode.OK, ban.StatusCode);
        Assert.Equal(HttpStatusCode.OK, unban.StatusCode);
        using var verificationScope = _factory.Services.CreateScope();
        var verificationDb = verificationScope.ServiceProvider.GetRequiredService<AppDbContext>();
        var verifiedUser = await verificationDb.Users.SingleAsync(x => x.Id == OtherUserId);
        Assert.False(verifiedUser.IsBanned);
        Assert.Null(verifiedUser.BanReason);
        Assert.Null(verifiedUser.RefreshToken);
    }

    [Fact]
    public async Task AdminCanChangeAnotherUsersRoleButCannotChangeOwnRole()
    {
        await SeedUsers();
        var change = await SendAs(
            HttpMethod.Put,
            $"/arzly/v1.0/admin/users/{OtherUserId}/role",
            "admin",
            TestAuthHandler.DefaultUserId,
            JsonContent.Create(new ChangeUserRoleRequest { Role = "support" }));
        var selfChange = await SendAs(
            HttpMethod.Put,
            $"/arzly/v1.0/admin/users/{TestAuthHandler.DefaultUserId}/role",
            "admin",
            TestAuthHandler.DefaultUserId,
            JsonContent.Create(new ChangeUserRoleRequest { Role = "user" }));

        Assert.Equal(HttpStatusCode.OK, change.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, selfChange.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var target = await userManager.FindByIdAsync(OtherUserId.ToString());
        Assert.NotNull(target);
        Assert.True(await userManager.IsInRoleAsync(target, "support"));
    }

    [Fact]
    public async Task AuditFeed_IsAdminOnlyAndContainsFilterableRoleChanges()
    {
        await SeedUsers();
        await SendAs(
            HttpMethod.Put,
            $"/arzly/v1.0/admin/users/{OtherUserId}/role",
            "admin",
            TestAuthHandler.DefaultUserId,
            JsonContent.Create(new ChangeUserRoleRequest { Role = "support" }));

        var support = await SendAs(
            HttpMethod.Get,
            "/arzly/v1.0/admin/audit",
            "support",
            OutsiderId);
        var admin = await SendAs(
            HttpMethod.Get,
            $"/arzly/v1.0/admin/audit?actionType={(int)ActivityActionType.UserRoleChanged}",
            "admin",
            TestAuthHandler.DefaultUserId);

        Assert.Equal(HttpStatusCode.Forbidden, support.StatusCode);
        Assert.Equal(HttpStatusCode.OK, admin.StatusCode);
        var entries = await admin.Content.ReadFromJsonAsync<List<UserActivityLogResponse>>();
        Assert.NotNull(entries);
        var entry = Assert.Single(entries);
        Assert.Equal(ActivityActionType.UserRoleChanged, entry.ActionType);
        Assert.Equal(OtherUserId.ToString(), entry.TargetId);
    }

    private async Task SeedUsers()
    {
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, TestAuthHandler.DefaultUserId);
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, OtherUserId);
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, OutsiderId);
    }

    private async Task<UserReport> SeedReport()
    {
        await SeedUsers();
        var report = new UserReport
        {
            Id = Guid.NewGuid(), ReporterId = TestAuthHandler.DefaultUserId,
            ReportedUserId = OtherUserId, Reason = ReportReasonType.Harassment,
            Notes = "Report under review"
        };
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.UserReports.Add(report);
        await db.SaveChangesAsync();
        return report;
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
