using Arzly.Shared.DTOs.Response.ChatMessage;
using Arzly.Shared.Enums;

namespace Arzly.Shared.DTOs.Response.Chat
{
    public class ChatResponse
    {
        public Guid Id { get; set; }
        public ChatRole ContextRole { get; set; }
        public bool IsArchived { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDiscontinued { get; set; }
        public DateTime LastActivity { get; set; }
        public Guid InitiatorId { get; set; }
        public Guid ReceiverId { get; set; }
        public Guid? ListingId { get; set; }
        public Guid? JobListingId { get; set; }
        public string? ListingTitle { get; set; }
        public string PersonName { get; set; } = string.Empty;
        public List<ChatMessageResponse>? Messages { get; set; }
    }
}
