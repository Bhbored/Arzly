using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.TicketMessage
{
    public class TicketMessageUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Message { get; set; } = string.Empty;

        public bool IsInternalNote { get; set; }
    }
}
