using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums;

namespace Arzly.Api.Infrastructure.Data.Entities
{
    public class Notification
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? UserId { get; set; } // NULL = Broadcast to all users
        public AppUser? User { get; set; }
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public NotificationSource Source { get; set; }
        public bool IsBroadcast { get; set; } = false;
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? DeepLink { get; set; } // e.g., "/chat/123", "/listing/456"
    }
}
