using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums;

namespace Arzly.Api.Infrastructure.Data.Entities
{
    public class Chat
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string InitiatorId { get; set; } = "";
        public string ReceiverId { get; set; } = "";
        public AppUser Initiator { get; set; } = null!;
        public AppUser Receiver { get; set; } = null!;
        public Guid? ListingId { get; set; }
        public Listing? Listing { get; set; }
        public ChatRole ContextRole { get; set; } = ChatRole.Neutral; // Buyer/Seller/Neutral
        public bool IsArchived { get; set; } = false;
        public DateTime LastActivity { get; set; } = DateTime.UtcNow;
        public virtual ICollection<ChatMessage>? Messages { get; set; } 
    }
}
