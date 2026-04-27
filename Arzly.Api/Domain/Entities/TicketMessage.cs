using Arzly.Api.Infrastructure.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities
{
    public class TicketMessage
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Ticket ID is required.")]
        public Guid TicketId { get; set; }

        [ForeignKey(nameof(TicketId))]
        public virtual Ticket Ticket { get; set; } = null!;

        [Required(ErrorMessage = "Sender ID is required.")]
        public string SenderId { get; set; } = string.Empty;

        [ForeignKey(nameof(SenderId))]
        public virtual AppUser Sender { get; set; } = null!;
        [Required(ErrorMessage = "Receiver ID is required.")]
        public string ReceiverId { get; set; } = string.Empty;

        [ForeignKey(nameof(ReceiverId))]
        public virtual AppUser Receiver { get; set; } = null!;
        [Required(ErrorMessage = "Message is required.")]
        [MaxLength(3000, ErrorMessage = "Message cannot exceed 3000 characters.")]
        public string Message { get; set; } = string.Empty;

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public bool IsInternalNote { get; set; }

    }
}
