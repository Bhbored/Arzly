using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.TicketAttachment
{
    public class TicketAttachmentAddRequest
    {
        [Required]
        public Guid TicketId { get; set; }

        [Required(ErrorMessage = "File URL is required.")]
        [Url(ErrorMessage = "File URL must be a valid URL.")]
        [MaxLength(2048, ErrorMessage = "File URL cannot exceed 2048 characters.")]
        public string FileUrl { get; set; } = string.Empty;

        [MaxLength(255, ErrorMessage = "File name cannot exceed 255 characters.")]
        public string? FileName { get; set; }

        [MaxLength(100, ErrorMessage = "Content type cannot exceed 100 characters.")]
        public string? ContentType { get; set; }

        public long FileSize { get; set; }
    }
}
