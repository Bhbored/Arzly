using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.UserProfile
{
    public class UserProfileUpdateRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_.]+$", ErrorMessage = "Public name can only contain letters, numbers, spaces, hyphens, underscores, and periods.")]
        public string? FullName { get; set; }

        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_.]+$", ErrorMessage = "Public name can only contain letters, numbers, spaces, hyphens, underscores, and periods.")]
        public string? PublicName { get; set; }

        [EmailAddress]
        [MaxLength(256)]
        public string? Email { get; set; }

        [Phone]
        [MaxLength(20)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain digits only")]
        public string? PhoneNumber { get; set; }

        public DateTime? LastActiveAt { get; set; }


        [Url]
        [MaxLength(2048)]
        public string? ProfileImageUrl { get; set; }

        [MaxLength(500)]
        public string? StoreDescription { get; set; }

        public bool IsStore { get; set; }

        [MaxLength(200)]
        public string? PublicLocation { get; set; }
    }
}
