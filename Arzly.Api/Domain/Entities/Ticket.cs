using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums.Ticket;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string Subject { get; set; } = string.Empty;

        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public TicketPriority Priority { get; set; } = TicketPriority.Medium;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        // Foreign keys
        [Required]
        public string UserId { get; set; } = string.Empty; 

        public string? AssignedToId { get; set; } 

        public Guid? ListingId { get; set; } 

        // Navigation
        [ForeignKey(nameof(UserId))]
        public virtual AppUser User { get; set; } = null!;

        [ForeignKey(nameof(AssignedToId))]
        public virtual AppUser? AssignedTo { get; set; }

        [ForeignKey(nameof(ListingId))]
        public virtual Listing? RelatedListing { get; set; }

        public virtual ICollection<TicketMessage>? Messages { get; set; }
        public virtual ICollection<TicketAttachment>? Attachments { get; set; }
    }
}
