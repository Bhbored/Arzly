using Arzly.Api.Domain.Entities.Communications;
using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities.Users
{
    public class UserReport
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Reporter ID is required.")]
        public Guid ReporterId { get; set; }

        [ForeignKey(nameof(ReporterId))]
        public virtual ApplicationUser Reporter { get; set; } = null!;

        [Required(ErrorMessage = "Reported user ID is required.")]
        public Guid ReportedUserId { get; set; }

        [ForeignKey(nameof(ReportedUserId))]
        public virtual ApplicationUser ReportedUser { get; set; } = null!;

        public Guid? ChatId { get; set; }

        [ForeignKey(nameof(ChatId))]
        public virtual Chat? Chat { get; set; }

        [Required(ErrorMessage = "Report reason is required.")]
        public ReportReasonType Reason { get; set; }

        [MaxLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsResolved { get; set; } = false;

        public Guid? ResolvedById { get; set; }

        public DateTime? ResolvedAt { get; set; }
    }
}
