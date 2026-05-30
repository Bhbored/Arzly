using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.TicketMessage
{
    public class TicketMessageAddRequest
    {
        [Required]
        public Guid TicketId { get; set; }

        [Required(ErrorMessage = "Sender ID is required.")]
        public Guid SenderId { get; set; }

        [Required(ErrorMessage = "Receiver ID is required.")]
        public Guid ReceiverId { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [MaxLength(3000, ErrorMessage = "Message cannot exceed 3000 characters.")]
        public string Message { get; set; } = string.Empty;

        public bool IsInternalNote { get; set; }
    }
}
