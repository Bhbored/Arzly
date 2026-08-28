using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Api.Infrastructure.Storage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Arzly.Tests.Helpers
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly string _databaseName = $"ArzlyTests-{Guid.NewGuid():N}";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            builder.UseSetting("Jwt:Key", "arzly-integration-test-signing-key-at-least-32-bytes");
            builder.UseSetting("Jwt:Issuer", "arzly-tests");
            builder.UseSetting("Jwt:Audience", "arzly-tests");
            builder.UseSetting("Jwt:EXPIRATION_MINUTES", "15");
            builder.UseSetting("RefreshToken:EXPIRATION_DAYS", "7");
            builder.UseSetting("AllowedOrigins:0", "https://admin.arzly.test");
            builder.UseSetting("RateLimits:Auth:PermitLimit", "5");
            builder.UseSetting("RateLimits:Auth:WindowSeconds", "60");
            builder.UseSetting("ReverseProxy:KnownProxies:0", "10.0.0.10");
            builder.UseSetting("ReverseProxy:KnownIPNetworks:0", "192.0.2.0/24");
            builder.UseSetting("CloudflareR2:AccessKey", "test-access-key");
            builder.UseSetting("CloudflareR2:SecretKey", "test-secret-key");
            builder.UseSetting("CloudflareR2:ServiceURL", "https://r2.arzly.test");
            builder.UseSetting("CloudflareR2:BucketName", "test-bucket");
            builder.UseSetting("CloudflareR2:PublicUrlBase", "https://uploads.arzly.test");
            builder.UseSetting("GoogleMaps:ApiKey", "test-maps-key");
            builder.UseSetting("Email:Host", "smtp.arzly.test");
            builder.UseSetting("Email:Port", "587");
            builder.UseSetting("Email:Username", "test@arzly.test");
            builder.UseSetting("Email:Password", "test-email-password");
            builder.UseSetting("Authentication:Google:ClientId", "test-google-client");
            builder.UseSetting("Authentication:Google:ClientSecret", "test-google-secret");

            builder.ConfigureServices(services =>
            {
                var dbDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (dbDescriptor is not null)
                    services.Remove(dbDescriptor);

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase(_databaseName);
                });

                var uploaderDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IImageUploader));
                if (uploaderDescriptor is not null)
                    services.Remove(uploaderDescriptor);
                services.AddSingleton<IImageUploader, TestImageUploader>();

                services.AddAuthentication(TestAuthHandler.SchemeName)
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.SchemeName, options => { });

                services.Configure<AuthenticationOptions>(options =>
                {
                    options.DefaultAuthenticateScheme = TestAuthHandler.SchemeName;
                    options.DefaultChallengeScheme = TestAuthHandler.SchemeName;
                });
            });
        }

        public void ResetDatabase()
        {
            var scopeFactory = Services.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }

        private sealed class TestImageUploader : IImageUploader
        {
            public Task<string> UploadFile(string userId, Stream fileStream, string fileName, CancellationToken cancellationToken = default) =>
                Task.FromResult($"https://uploads.arzly.test/{userId}/{fileName}");

            public Task<List<string>> UploadFiles(
                string userId,
                List<(Stream Stream, string FileName)> files,
                CancellationToken cancellationToken = default) =>
                Task.FromResult(files
                    .Select(file => $"https://uploads.arzly.test/{userId}/{file.FileName}")
                    .ToList());

            public Task<bool> DeleteFile(
                string userId,
                string fileUrl,
                CancellationToken cancellationToken = default) =>
                Task.FromResult(IsOwnedFileUrl(userId, fileUrl));

            public bool IsOwnedFileUrl(string userId, string fileUrl) =>
                Uri.TryCreate(fileUrl, UriKind.Absolute, out var uri) &&
                uri.Host == "uploads.arzly.test" &&
                uri.AbsolutePath.StartsWith($"/{userId}/", StringComparison.Ordinal);
        }
    }
}
