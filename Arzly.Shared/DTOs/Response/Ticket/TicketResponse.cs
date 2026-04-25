using Arzly.Shared.Enums.Ticket;

namespace Arzly.Shared.DTOs.Response.Ticket
{
    public class TicketResponse
    {
        public Guid Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? AssignedToId { get; set; }
        public Guid? ListingId { get; set; }
    }
}
