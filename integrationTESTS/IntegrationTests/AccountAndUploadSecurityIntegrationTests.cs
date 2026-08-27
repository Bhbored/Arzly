using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Arzly.Api.Domain.Entities.Users;
using Arzly.Api.Application.Contracts.Auth;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.DTOs.Request.Auth;
using Arzly.Shared.DTOs.Response.Auth;
using Arzly.Shared.DTOs.Request.UserProfile;
using Arzly.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace Arzly.IntegrationTests;

public class AccountAndUploadSecurityIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private static readonly Guid OtherUserId =
        Guid.Parse("20000000-0000-0000-0000-000000000002");

    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public AccountAndUploadSecurityIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _factory.ResetDatabase();
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Logout_RequiresAuthentication()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/arzly/v1.0/Authentication/logout");
        request.Headers.Add(TestAuthHandler.AuthenticationHeader, "anonymous");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Logout_RevokesStoredRefreshToken()
    {
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, TestAuthHandler.DefaultUserId);
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var user = await db.Users.SingleAsync(x => x.Id == TestAuthHandler.DefaultUserId);
            user.RefreshToken = "active-refresh-token";
            user.RefreshTokenExpirateDate = DateTime.UtcNow.AddDays(1);
            await db.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/arzly/v1.0/Authentication/logout");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        using var verificationScope = _factory.Services.CreateScope();
        var verificationDb = verificationScope.ServiceProvider.GetRequiredService<AppDbContext>();
        var updated = await verificationDb.Users.SingleAsync(x => x.Id == TestAuthHandler.DefaultUserId);
        Assert.Null(updated.RefreshToken);
        Assert.Null(updated.RefreshTokenExpirateDate);
    }

    [Fact]
    public async Task RefreshToken_RotatesAndCannotBeReused()
    {
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, TestAuthHandler.DefaultUserId);
        AuthenticationResponse original;
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var jwt = scope.ServiceProvider.GetRequiredService<IJwtService>();
            var user = await db.Users.SingleAsync(x => x.Id == TestAuthHandler.DefaultUserId);
            original = jwt.CreateJwtToken(user, "user");
            user.RefreshToken = original.RefreshToken;
            user.RefreshTokenExpirateDate = original.RefreshTokenExpirateDate;
            await db.SaveChangesAsync();
        }
        var tokenRequest = new TokenModel
        {
            Token = original.Token,
            RefreshToken = original.RefreshToken
        };

        var first = await _client.PostAsJsonAsync(
            "/arzly/v1.0/Authentication/generate-new-jwt-token",
            tokenRequest);
        var replay = await _client.PostAsJsonAsync(
            "/arzly/v1.0/Authentication/generate-new-jwt-token",
            tokenRequest);

        Assert.Equal(HttpStatusCode.OK, first.StatusCode);
        var rotated = await first.Content.ReadFromJsonAsync<AuthenticationResponse>();
        Assert.NotNull(rotated);
        Assert.NotEqual(original.RefreshToken, rotated.RefreshToken);
        Assert.Equal(HttpStatusCode.Unauthorized, replay.StatusCode);
    }

    [Fact]
    public async Task Login_RejectsActivelyBannedAccount()
    {
        const string email = "banned@arzly.test";
        const string password = "TestPassword1!";
        using (var scope = _factory.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                IsBanned = true,
                BanReason = "Integration test",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var created = await userManager.CreateAsync(user, password);
            Assert.True(created.Succeeded, string.Join(" | ", created.Errors.Select(x => x.Description)));
        }

        var response = await _client.PostAsJsonAsync(
            "/arzly/v1.0/Authentication/login",
            new LoginDTO { Email = email, Password = password });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ProfileUpdate_CannotTargetAnotherUser()
    {
        await SeedProfiles();
        var request = new UserProfileUpdateRequest
        {
            UserId = OtherUserId,
            FullName = "Taken over",
            PublicName = "attacker"
        };

        var response = await _client.PutAsJsonAsync("/arzly/v1.0/UserProfile/Update", request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var profile = await db.UserProfiles.SingleAsync(x => x.UserId == OtherUserId);
        Assert.Equal("Other user", profile.FullName);
    }

    [Fact]
    public async Task UploadImage_RejectsUnsupportedContentBeforeStorageCall()
    {
        using var content = new MultipartFormDataContent();
        var file = new ByteArrayContent("not an image"u8.ToArray());
        file.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
        content.Add(file, "file", "payload.txt");

        var response = await _client.PostAsync("/arzly/v1.0/Upload/upload-image", content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UploadImage_RejectsRenamedNonImageContent()
    {
        using var content = new MultipartFormDataContent();
        var file = new ByteArrayContent("not really a png"u8.ToArray());
        file.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        content.Add(file, "file", "payload.png");

        var response = await _client.PostAsync("/arzly/v1.0/Upload/upload-image", content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UploadImage_AcceptsContentWithMatchingImageSignature()
    {
        using var content = new MultipartFormDataContent();
        var file = new ByteArrayContent([0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A]);
        file.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        content.Add(file, "file", "payload.png");

        var response = await _client.PostAsync("/arzly/v1.0/Upload/upload-image", content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UploadImages_RejectsMoreThanTenFilesBeforeStorageCall()
    {
        using var content = new MultipartFormDataContent();
        for (var index = 0; index < 11; index++)
        {
            var file = new ByteArrayContent([1]);
            file.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            content.Add(file, "files", $"image-{index}.png");
        }

        var response = await _client.PostAsync("/arzly/v1.0/Upload/upload-images", content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    private async Task SeedProfiles()
    {
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, TestAuthHandler.DefaultUserId);
        await TestDataSeeder.SeedUserWithPickupLocation(_factory, OtherUserId);
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.UserProfiles.AddRange(
            new UserProfile { UserId = TestAuthHandler.DefaultUserId, FullName = "Current user" },
            new UserProfile { UserId = OtherUserId, FullName = "Other user" });
        await db.SaveChangesAsync();
    }
}
