namespace Arzly.Shared.DTOs.Request.TicketMessage
{
    public class TicketMessageAddRequest
    {
        public Guid TicketId { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsInternalNote { get; set; }
    }
}
