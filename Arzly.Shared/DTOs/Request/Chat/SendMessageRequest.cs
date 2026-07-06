using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.Chat
{
    public class SendMessageRequest
    {
        [Required(ErrorMessage = "Chat ID is required.")]
        public Guid ChatId { get; set; }

        [Required(ErrorMessage = "Message text is required.")]
        [MaxLength(2000, ErrorMessage = "Message cannot exceed 2000 characters.")]
        public string Text { get; set; } = string.Empty;
    }
}
