using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.User
{
    public class AppUserUpdateRequest
    {
        [Required]
        public string Id { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? UserName { get; set; }

        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_.]+$", ErrorMessage = "Public name can only contain letters, numbers, spaces, hyphens, underscores, and periods.")]
        public string? PublicName { get; set; }

        [MaxLength(200)]
        public string? PublicLocation { get; set; }

        [Url]
        [MaxLength(2048)]
        public string? ProfileImageUrl { get; set; }

        public DateTime? LastActiveAt { get; set; }

        public bool IsBanned { get; set; }

        [MaxLength(500)]
        public string? BanReason { get; set; }

        public DateTime? BanExpiresAt { get; set; }

        public bool IsVerified { get; set; }
    }
}
