namespace Arzly.Shared.DTOs.Response.TicketAttachment
{
    public class TicketAttachmentResponse
    {
        public Guid Id { get; set; }
        public Guid TicketId { get; set; }
        public string FileUrl { get; set; } = string.Empty;
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
