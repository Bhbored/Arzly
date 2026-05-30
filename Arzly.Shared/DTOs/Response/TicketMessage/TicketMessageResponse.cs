namespace Arzly.Shared.DTOs.Response.TicketMessage
{
    public class TicketMessageResponse
    {
        public Guid Id { get; set; }
        public Guid TicketId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public bool IsInternalNote { get; set; }
    }
}
