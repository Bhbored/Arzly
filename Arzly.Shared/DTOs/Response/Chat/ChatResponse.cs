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
        public string InitiatorId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public Guid ListingId { get; set; }
    }
}
