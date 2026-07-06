using Arzly.Api.Infrastructure.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities.Communications
{
    public class ChatMessage
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Chat ID is required.")]
        public Guid ChatId { get; set; }

      

        [Required(ErrorMessage = "Sender ID is required.")]
        public Guid SenderId { get; set; }


        [Required(ErrorMessage = "Receiver ID is required.")]
        public Guid ReceiverId { get; set; }


        [Required(ErrorMessage = "Message text is required.")]
        [MaxLength(2000, ErrorMessage = "Message cannot exceed 2000 characters.")]
        public string Text { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public bool IsRead { get; set; }=false;
        public DateTime? ReadAt { get; set; }


        [ForeignKey(nameof(ReceiverId))]
        public virtual ApplicationUser Receiver { get; set; } = null!;

        [ForeignKey(nameof(SenderId))]
        public virtual ApplicationUser Sender { get; set; } = null!;
        [ForeignKey(nameof(ChatId))]
        public virtual Chat Chat { get; set; } = null!;

    }
}
