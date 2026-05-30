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

        [Required(ErrorMessage = "Subject is required.")]
        [MaxLength(200, ErrorMessage = "Subject cannot exceed 200 characters.")]
        public string Subject { get; set; } = string.Empty;

        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public TicketPriority Priority { get; set; } = TicketPriority.Medium;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        // Foreign keys
        [Required(ErrorMessage = "User ID is required.")]
        public Guid UserId { get; set; } 

        public Guid? AssignedToId { get; set; } 

        public Guid? ListingId { get; set; } 

        // Navigation
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(AssignedToId))]
        public virtual ApplicationUser? AssignedTo { get; set; }

        [ForeignKey(nameof(ListingId))]
        public virtual Listing? RelatedListing { get; set; }

        public virtual ICollection<TicketMessage>? Messages { get; set; }
        public virtual ICollection<TicketAttachment>? Attachments { get; set; }
    }
}
