using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities
{
    public class UserReport
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string ReporterId { get; set; } = string.Empty;

        [ForeignKey(nameof(ReporterId))]
        public virtual AppUser Reporter { get; set; } = null!;

        [Required]
        public string ReportedUserId { get; set; } = string.Empty;

        [ForeignKey(nameof(ReportedUserId))]
        public virtual AppUser ReportedUser { get; set; } = null!;

        public Guid? ChatId { get; set; }

        [ForeignKey(nameof(ChatId))]
        public virtual Chat? Chat { get; set; }

        [Required]
        public ReportReasonType Reason { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsResolved { get; set; } = false;

        public string? ResolvedById { get; set; }

        public DateTime? ResolvedAt { get; set; }
    }
}
