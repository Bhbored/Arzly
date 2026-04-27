using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.TicketMessage
{
    public class TicketMessageUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [MaxLength(3000, ErrorMessage = "Message cannot exceed 3000 characters.")]
        public string Message { get; set; } = string.Empty;

        public bool IsInternalNote { get; set; }
    }
}
