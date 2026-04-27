using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class UserSeed
    {
        public static readonly List<AppUser> Data = new()
        {
            new AppUser 
            { 
                Id = "7c9e6679-7425-40de-944b-e07fc1f90ae7", 
                UserName = "user1@arzly.com", 
                NormalizedUserName = "USER1@ARZLY.COM", 
                Email = "user1@arzly.com", 
                NormalizedEmail = "USER1@ARZLY.COM", 
                PublicName = "User One", 
                AuthMethod = AuthMethod.Firebase,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new AppUser 
            { 
                Id = "8f3b2a1c-5d4e-4f3a-9b8c-7d6e5f4a3b2c", 
                UserName = "user2@arzly.com", 
                NormalizedUserName = "USER2@ARZLY.COM", 
                Email = "user2@arzly.com", 
                NormalizedEmail = "USER2@ARZLY.COM", 
                PublicName = "User Two", 
                AuthMethod = AuthMethod.Identity,
                CreatedAt = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc)
            },
            new AppUser 
            { 
                Id = "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d", 
                UserName = "user3@arzly.com", 
                NormalizedUserName = "USER3@ARZLY.COM", 
                Email = "user3@arzly.com", 
                NormalizedEmail = "USER3@ARZLY.COM", 
                PublicName = "User Three", 
                AuthMethod = AuthMethod.Firebase,
                CreatedAt = new DateTime(2024, 1, 3, 0, 0, 0, DateTimeKind.Utc)
            }
        };
    }
}
