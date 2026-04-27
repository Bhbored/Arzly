using Arzly.Shared.Enums.Ticket;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.Ticket
{
    public class TicketUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        [MaxLength(200, ErrorMessage = "Subject cannot exceed 200 characters.")]
        public string Subject { get; set; } = string.Empty;

        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public string? AssignedToId { get; set; }
        public DateTime? ClosedAt { get; set; }
    }
}
