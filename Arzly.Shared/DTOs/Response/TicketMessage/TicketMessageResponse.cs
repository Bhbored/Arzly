namespace Arzly.Shared.DTOs.Response.TicketMessage
{
    public class TicketMessageResponse
    {
        public Guid Id { get; set; }
        public Guid TicketId { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public bool IsInternalNote { get; set; }
    }
}
