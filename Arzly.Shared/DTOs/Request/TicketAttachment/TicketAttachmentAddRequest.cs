using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.TicketAttachment
{
    public class TicketAttachmentAddRequest
    {
        [Required]
        public Guid TicketId { get; set; }

        [Required]
        [Url]
        [MaxLength(2048)]
        public string FileUrl { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? FileName { get; set; }

        [MaxLength(100)]
        public string? ContentType { get; set; }

        public long FileSize { get; set; }
    }
}
