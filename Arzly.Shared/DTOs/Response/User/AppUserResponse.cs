using Arzly.Shared.Enums;

namespace Arzly.Shared.DTOs.Response.User
{
    public class AppUserResponse
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public AuthMethod AuthMethod { get; set; }
        public string? FirebaseUid { get; set; }
        public string? PublicName { get; set; }
        public string? PublicLocation { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastActiveAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsBanned { get; set; }
        public string? BanReason { get; set; }
        public DateTime? BanExpiresAt { get; set; }
        public bool IsVerified { get; set; }
    }
}
