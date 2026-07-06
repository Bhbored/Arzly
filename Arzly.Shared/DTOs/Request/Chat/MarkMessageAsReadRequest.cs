using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.Chat
{
    public class MarkMessageAsReadRequest
    {
        [Required]
        public Guid MessageId { get; set; }
    }
}
