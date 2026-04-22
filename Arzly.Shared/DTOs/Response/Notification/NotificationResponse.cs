using Arzly.Shared.Enums;

namespace Arzly.Shared.DTOs.Response.Notification
{
    public class NotificationResponse
    {
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public Guid? ChatterId { get; set; }
        public Guid? ListingId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public NotificationSource Source { get; set; }
        public bool IsBroadcast { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public string? DeepLink { get; set; }
        public NotificationActionType ActionType { get; set; }
        public string? Metadata { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
