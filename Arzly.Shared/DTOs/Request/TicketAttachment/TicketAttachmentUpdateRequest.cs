namespace Arzly.Shared.DTOs.Request.TicketAttachment
{
    public class TicketAttachmentUpdateRequest
    {
        public Guid Id { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
    }
}
