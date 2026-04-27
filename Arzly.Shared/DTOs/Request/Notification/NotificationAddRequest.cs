using Arzly.Shared.Enums.Notification;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.Notification
{
    public class NotificationAddRequest
    {
        public string? UserId { get; set; }
        public Guid? ChatterId { get; set; }
        public Guid? ListingId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Body is required.")]
        [MaxLength(1000, ErrorMessage = "Body cannot exceed 1000 characters.")]
        public string Body { get; set; } = string.Empty;

        public NotificationSource Source { get; set; }
        public bool IsBroadcast { get; set; }

        [MaxLength(500, ErrorMessage = "Deep link cannot exceed 500 characters.")]
        public string? DeepLink { get; set; }

        public NotificationActionType ActionType { get; set; }
        public string? Metadata { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
