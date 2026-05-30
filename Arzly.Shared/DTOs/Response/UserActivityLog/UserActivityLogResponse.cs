
using Arzly.Shared.Enums.Activity;

namespace Arzly.Shared.DTOs.Response.UserActivityLog
{
    public class UserActivityLogResponse
    {
        public Guid Id { get; set; }
        public Guid ActorId { get; set; }
        public string ActorRole { get; set; } = string.Empty;
        public ActivityActionType ActionType { get; set; }
        public ActivityTargetType TargetType { get; set; }
        public string? TargetId { get; set; }
        public string? IPAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? Details { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public int? DurationMs { get; set; }
    }
}
