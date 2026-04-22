namespace Arzly.Shared.DTOs.Request.ChatMessage
{
    public class ChatMessageAddRequest
    {
        public Guid ChatId { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}
