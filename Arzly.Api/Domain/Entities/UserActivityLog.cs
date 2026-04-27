using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums.Activity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities
{
    public class UserActivityLog
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Actor ID is required.")]
        public string ActorId { get; set; } = string.Empty;

        [ForeignKey(nameof(ActorId))]
        public virtual AppUser Actor { get; set; } = null!;

        [Required(ErrorMessage = "Actor role is required.")]
        [MaxLength(50, ErrorMessage = "Actor role cannot exceed 50 characters.")]
        public string ActorRole { get; set; } = string.Empty;

        [Required(ErrorMessage = "Activity action type is required.")]
        public ActivityActionType ActionType { get; set; }

        public ActivityTargetType TargetType { get; set; } = ActivityTargetType.None;

        [MaxLength(100, ErrorMessage = "Target ID cannot exceed 100 characters.")]
        public string? TargetId { get; set; }

        [MaxLength(45, ErrorMessage = "IP address cannot exceed 45 characters.")]
        public string? IPAddress { get; set; }

        public string? UserAgent { get; set; }

        public string? Details { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public bool IsSuccess { get; set; } = true;

        public string? ErrorMessage { get; set; }

        public int? DurationMs { get; set; }
    }
}
