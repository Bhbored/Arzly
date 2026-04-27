using Arzly.Shared.Enums.Ticket;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.Ticket
{
    public class TicketAddRequest
    {
        [Required(ErrorMessage = "Subject is required.")]
        [MaxLength(200, ErrorMessage = "Subject cannot exceed 200 characters.")]
        public string Subject { get; set; } = string.Empty;

        public TicketPriority Priority { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; } = string.Empty;

        public Guid? ListingId { get; set; }
    }
}
