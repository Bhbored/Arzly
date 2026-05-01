using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class AppUserSeed
    {
        public static readonly List<AppUser> Users = new()
        {
            new AppUser
        {
            Id = "user-1-id",
            UserName = "john_doe",
            Email = "john@example.com",
            AuthMethod = AuthMethod.Firebase,
            FirebaseUid = "firebase-uid-123",
            PublicName = "John Doe",
            PublicLocation = "New York, USA",
            ProfileImageUrl = "https://example.com/profiles/john.jpg",
            CreatedAt = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc),
            LastActiveAt = new DateTime(2025, 4, 1, 12, 0, 0, DateTimeKind.Utc),
        }, new AppUser
        {
            Id = "user-2-id",
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
