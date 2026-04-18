using Arzly.Api.Infrastructure.Identity;

namespace Arzly.Api.Infrastructure.Data.Entities
{
    public class ChatMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; } = null!;
        public string SenderId { get; set; } = "";
        public AppUser Sender { get; set; } = null!;
        public string Text { get; set; } = "";
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }
    }
}
