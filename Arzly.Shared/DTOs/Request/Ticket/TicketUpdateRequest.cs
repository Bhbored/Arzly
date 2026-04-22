using Arzly.Shared.Enums;

namespace Arzly.Shared.DTOs.Request.Ticket
{
    public class TicketUpdateRequest
    {
        public Guid Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public string? AssignedToId { get; set; }
        public DateTime? ClosedAt { get; set; }
    }
}
