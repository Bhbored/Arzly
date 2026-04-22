namespace Arzly.Shared.DTOs.Request.TicketAttachment
{
    public class TicketAttachmentAddRequest
    {
        public Guid TicketId { get; set; }
        public string FileUrl { get; set; } = string.Empty;
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public long FileSize { get; set; }
    }
}
