using Arzly.Shared.Enums.Notification;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.Notification
{
    public class NotificationAddRequest
    {
        public string? UserId { get; set; }
        public Guid? ChatterId { get; set; }
        public Guid? ListingId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Body { get; set; } = string.Empty;

        public NotificationSource Source { get; set; }
        public bool IsBroadcast { get; set; }

        [MaxLength(500)]
        public string? DeepLink { get; set; }

        public NotificationActionType ActionType { get; set; }
        public string? Metadata { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
