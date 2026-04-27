using Arzly.Shared.Enums.Activity;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.UserActivityLog
{
    public class UserActivityLogAddRequest
    {
        [Required]
        public string ActorId { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string ActorRole { get; set; } = string.Empty;

        [Required]
        public ActivityActionType ActionType { get; set; }

        public ActivityTargetType TargetType { get; set; }

        [MaxLength(100)]
        public string? TargetId { get; set; }

        [MaxLength(45)]
        public string? IPAddress { get; set; }

        public string? UserAgent { get; set; }
        public string? Details { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public int? DurationMs { get; set; }
    }
}
