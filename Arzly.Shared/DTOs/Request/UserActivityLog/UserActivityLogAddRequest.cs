using Arzly.Shared.Enums;

namespace Arzly.Shared.DTOs.Request.UserActivityLog
{
    public class UserActivityLogAddRequest
    {
        public string ActorId { get; set; } = string.Empty;
        public string ActorRole { get; set; } = string.Empty;
        public ActivityActionType ActionType { get; set; }
        public ActivityTargetType TargetType { get; set; }
        public string? TargetId { get; set; }
        public string? IPAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? Details { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public int? DurationMs { get; set; }
    }
}
