using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.ChatMessage
{
    public class ChatMessageAddRequest
    {
        [Required(ErrorMessage = "Chat ID is required.")]
        public Guid ChatId { get; set; }

        [Required(ErrorMessage = "Sender ID is required.")]
        public string SenderId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Receiver ID is required.")]
        public string ReceiverId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Message text is required.")]
        [MaxLength(2000, ErrorMessage = "Message cannot exceed 2000 characters.")]
        public string Text { get; set; } = string.Empty;
    }
}
