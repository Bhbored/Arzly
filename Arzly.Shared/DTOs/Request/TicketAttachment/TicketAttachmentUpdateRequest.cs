using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.TicketAttachment
{
    public class TicketAttachmentUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [MaxLength(255, ErrorMessage = "File name cannot exceed 255 characters.")]
        public string? FileName { get; set; }

        [MaxLength(100, ErrorMessage = "Content type cannot exceed 100 characters.")]
        public string? ContentType { get; set; }
    }
}
