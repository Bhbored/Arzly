using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.ChatMessage
{
    public class ChatMessageUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Text { get; set; } = string.Empty;

        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
