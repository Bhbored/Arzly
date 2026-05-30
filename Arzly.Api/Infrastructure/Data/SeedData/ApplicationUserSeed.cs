using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class ApplicationUserSeed
    {
        public static readonly List<ApplicationUser> Users = new()
        {
            new ApplicationUser
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                UserName = "john_doe",
                Email = "john@example.com",
                AuthMethod = AuthMethod.Firebase,
                FirebaseUid = "firebase-uid-123",
                PublicName = "John Doe",
                PublicLocation = "New York, USA",
                ProfileImageUrl = "https://example.com/profiles/john.jpg",
                CreatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc),
                LastActiveAt = new DateTime(2025, 4, 1, 12, 0, 0, DateTimeKind.Utc),
            },
            new ApplicationUser
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                UserName = "bourhan-hassoun",
                Email = "bhbored2022@gmail.com",
                AuthMethod = AuthMethod.Firebase,
                FirebaseUid = "firebase-uid-124",
                PublicName = "John Doe",
                PublicLocation = "New York, USA",
                ProfileImageUrl = "https://example.com/profiles/john.jpg",
                CreatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc),
                LastActiveAt = new DateTime(2025, 4, 1, 12, 0, 0, DateTimeKind.Utc),
            }
        };
    }
}
