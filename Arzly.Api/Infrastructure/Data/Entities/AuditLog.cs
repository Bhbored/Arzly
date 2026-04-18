using Arzly.Api.Infrastructure.Identity;

namespace Arzly.Api.Infrastructure.Data.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string AdminId { get; set; } = "";
        public AppUser Admin { get; set; } = null!;
        public string ActionType { get; set; } = ""; // e.g., "ApproveListing", "BroadcastNotification", "SuspendUser"
        public string TargetType { get; set; } = ""; // e.g., "Listing", "User", "Chat"
        public string? TargetId { get; set; }
        public string? Details { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
