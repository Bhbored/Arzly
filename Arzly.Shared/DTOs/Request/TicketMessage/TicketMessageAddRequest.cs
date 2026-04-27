using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.TicketMessage
{
    public class TicketMessageAddRequest
    {
        [Required]
        public Guid TicketId { get; set; }

        [Required]
        public string SenderId { get; set; } = string.Empty;

        [Required]
        public string ReceiverId { get; set; } = string.Empty;

        [Required]
        [MaxLength(3000)]
        public string Message { get; set; } = string.Empty;

        public bool IsInternalNote { get; set; }
    }
}
