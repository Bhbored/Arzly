using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.TicketAttachment
{
    public class TicketAttachmentUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string? FileName { get; set; }

        [MaxLength(100)]
        public string? ContentType { get; set; }
    }
}
