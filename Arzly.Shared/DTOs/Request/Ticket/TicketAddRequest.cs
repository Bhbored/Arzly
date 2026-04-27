using Arzly.Shared.Enums.Ticket;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.Ticket
{
    public class TicketAddRequest
    {
        [Required]
        [MaxLength(200)]
        public string Subject { get; set; } = string.Empty;

        public TicketPriority Priority { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public Guid? ListingId { get; set; }
    }
}
