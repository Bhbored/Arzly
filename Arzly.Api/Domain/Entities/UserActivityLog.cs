using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities
{
    public class UserActivityLog
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string ActorId { get; set; } = string.Empty;

        [ForeignKey(nameof(ActorId))]
        public virtual AppUser Actor { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string ActorRole { get; set; } = string.Empty;

        [Required]
        public ActivityActionType ActionType { get; set; }

        public ActivityTargetType TargetType { get; set; } = ActivityTargetType.None;

        [MaxLength(100)]
        public string? TargetId { get; set; }

        [MaxLength(45)]
        public string? IPAddress { get; set; }

        public string? UserAgent { get; set; }

        public string? Details { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public bool IsSuccess { get; set; } = true;

        public string? ErrorMessage { get; set; }

        public int? DurationMs { get; set; }
    }
}
