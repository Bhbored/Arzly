using Arzly.Shared.Enums.Notification;

namespace Arzly.Shared.DTOs.Request.Notification
{
    public class NotificationAddRequest
    {
        public string? UserId { get; set; }
        public Guid? ChatterId { get; set; }
        public Guid? ListingId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public NotificationSource Source { get; set; }
        public bool IsBroadcast { get; set; }
        public string? DeepLink { get; set; }
        public NotificationActionType ActionType { get; set; }
        public string? Metadata { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
