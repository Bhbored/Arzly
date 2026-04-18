using Arzly.Api.Infrastructure.Identity;

namespace Arzly.Api.Infrastructure.Data.Entities
{
    public class UserActivityLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Who did it (any user: User/Admin/Support)
        public string ActorId { get; set; } = "";
        public AppUser Actor { get; set; } = null!;

        // What happened
        public string ActionType { get; set; } = ""; // "Login", "CreatedListing", "SentMessage", "ApprovedListing"
        public string? TargetType { get; set; }      // "Listing", "Chat", "Notification"
        public string? TargetId { get; set; }

        // Context (optional but useful)
        public string? IPAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? Details { get; set; }         // JSON for extra context

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
