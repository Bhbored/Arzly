namespace Arzly.Shared.DTOs.Response.UserProfile
{
    public class UserProfileResponse
    {
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public string? PublicName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? StoreDescription { get; set; }
        public bool IsStore { get; set; }
        public string? PublicLocation { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastActiveAt { get; set; }
    }
}
