using Arzly.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.User
{
    public class AppUserAddRequest
    {
        [Required(ErrorMessage = "User name is required.")]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        public AuthMethod AuthMethod { get; set; } = AuthMethod.Firebase;

        [MaxLength(128)]
        public string? FirebaseUid { get; set; }

        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_.]+$", ErrorMessage = "Public name can only contain letters, numbers, spaces, hyphens, underscores, and periods.")]
        public string? PublicName { get; set; }

        [MaxLength(200)]
        public string? PublicLocation { get; set; }

        [Url]
        [MaxLength(2048)]
        public string? ProfileImageUrl { get; set; }
    }
}
