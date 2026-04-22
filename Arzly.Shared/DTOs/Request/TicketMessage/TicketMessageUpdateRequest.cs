namespace Arzly.Shared.DTOs.Request.TicketMessage
{
    public class TicketMessageUpdateRequest
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsInternalNote { get; set; }
    }
}
