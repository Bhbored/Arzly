using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities
{
    public class TicketAttachment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Ticket ID is required.")]
        public Guid TicketId { get; set; }

        [ForeignKey(nameof(TicketId))]
        public virtual Ticket Ticket { get; set; } = null!;

        [Required(ErrorMessage = "File URL is required.")]
        [Url(ErrorMessage = "File URL must be a valid URL.")]
        [MaxLength(2048, ErrorMessage = "File URL cannot exceed 2048 characters.")]
        public string FileUrl { get; set; } = string.Empty;

        [MaxLength(255, ErrorMessage = "File name cannot exceed 255 characters.")]
        public string? FileName { get; set; }

        [MaxLength(100, ErrorMessage = "Content type cannot exceed 100 characters.")]
        public string? ContentType { get; set; }

        public long FileSize { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
