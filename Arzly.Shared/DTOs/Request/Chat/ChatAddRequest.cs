using Arzly.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.Chat
{
    public class ChatAddRequest
    {
        [Required]
        public ChatRole ContextRole { get; set; }

        [Required]
        public string InitiatorId { get; set; } = string.Empty;

        [Required]
        public string ReceiverId { get; set; } = string.Empty;

        public Guid? ListingId { get; set; }
    }
}
