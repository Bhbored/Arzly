using Arzly.Api.Infrastructure.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities
{
    public class ChatMessage
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Chat ID is required.")]
        public Guid ChatId { get; set; }

        [ForeignKey(nameof(ChatId))]
        public virtual Chat Chat { get; set; } = null!;

        [Required(ErrorMessage = "Sender ID is required.")]
        public string SenderId { get; set; } = string.Empty;

        [ForeignKey(nameof(SenderId))]
        public virtual AppUser Sender { get; set; } = null!;
        [Required(ErrorMessage = "Receiver ID is required.")]
        public string ReceiverId { get; set; } = string.Empty;
        [ForeignKey(nameof(ReceiverId))]
        public virtual AppUser Receiver { get; set; } = null!;

        [Required(ErrorMessage = "Message text is required.")]
        [MaxLength(2000, ErrorMessage = "Message cannot exceed 2000 characters.")]
        public string Text { get; set; } = string.Empty;

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
