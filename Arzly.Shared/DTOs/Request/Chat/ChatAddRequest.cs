using Arzly.Shared.Enums;

namespace Arzly.Shared.DTOs.Request.Chat
{
    public class ChatAddRequest
    {
        public ChatRole ContextRole { get; set; }
        public string InitiatorId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public Guid ListingId { get; set; }
    }
}
