using Arzly.Shared.Enums.Ticket;

namespace Arzly.Shared.DTOs.Request.Ticket
{
    public class TicketAddRequest
    {
        public string Subject { get; set; } = string.Empty;
        public TicketPriority Priority { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Guid? ListingId { get; set; }
    }
}
