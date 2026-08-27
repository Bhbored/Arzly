using Arzly.Api.Domain.Entities.Users;
using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums;
using Microsoft.AspNetCore.Identity;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class UserSeed
    {
        public static readonly Guid AdminId = Guid.Parse("00000000-0000-0000-0000-00000000000A");
        public static readonly Guid SupportId = Guid.Parse("00000000-0000-0000-0000-00000000000B");

        private static readonly Guid AdminRoleId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        private static readonly Guid SupportRoleId = Guid.Parse("00000000-0000-0000-0000-000000000002");

        public static readonly ApplicationUser[] Users =
        [
            new()
            {
                Id = AdminId,
                UserName = "bourhan-admin@gmail.com",
                NormalizedUserName = "BOURHAN-ADMIN@GMAIL.COM",
                Email = "bourhan-admin@gmail.com",
                NormalizedEmail = "BOURHAN-ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEAN/OwCjwTSTKWuGvMdIbh4RP/Pb6CxIXOJ6pJOSbrRU9I9dy9UIHeWFB7N2G0yqow==",
                SecurityStamp = "ADMIN-SEED-STAMP",
                AuthMethod = AuthMethod.Email,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = SupportId,
                UserName = "bourhan-support@gmail.com",
                NormalizedUserName = "BOURHAN-SUPPORT@GMAIL.COM",
                Email = "bourhan-support@gmail.com",
                NormalizedEmail = "BOURHAN-SUPPORT@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEJqqzmJDfcitN17c37+dIhknC8cUFSKBicxS7Wc/mfMugQZw3qJVIoRcLAcOtR0RNA==",
                SecurityStamp = "SUPPORT-SEED-STAMP",
                AuthMethod = AuthMethod.Email,
                CreatedAt = DateTime.UtcNow
            }
        ];

        public static readonly IdentityUserRole<Guid>[] UserRoles =
        [
            new() { UserId = AdminId, RoleId = AdminRoleId },
            new() { UserId = SupportId, RoleId = SupportRoleId }
        ];

        public static readonly UserProfile[] Profiles =
        [
            new() { UserId = AdminId, FullName = "Arzly Admin", Email = "bourhan-admin@gmail.com" },
            new() { UserId = SupportId, FullName = "Arzly Support", Email = "bourhan-support@gmail.com" }
        ];
    }
}
