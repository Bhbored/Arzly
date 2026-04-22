namespace Arzly.Shared.DTOs.Request.Chat
{
    public class ChatUpdateRequest
    {
        public Guid Id { get; set; }
        public bool IsArchived { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDiscontinued { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
