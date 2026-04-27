using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.ChatMessage
{
    public class ChatMessageUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Message text is required.")]
        [MaxLength(2000, ErrorMessage = "Message cannot exceed 2000 characters.")]
        public string Text { get; set; } = string.Empty;

        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
