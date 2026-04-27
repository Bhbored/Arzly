using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.ChatMessage
{
    public class ChatMessageAddRequest
    {
        [Required]
        public Guid ChatId { get; set; }

        [Required]
        public string SenderId { get; set; } = string.Empty;

        [Required]
        public string ReceiverId { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Text { get; set; } = string.Empty;
    }
}
