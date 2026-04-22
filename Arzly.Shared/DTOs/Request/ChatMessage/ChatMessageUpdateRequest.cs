namespace Arzly.Shared.DTOs.Request.ChatMessage
{
    public class ChatMessageUpdateRequest
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
