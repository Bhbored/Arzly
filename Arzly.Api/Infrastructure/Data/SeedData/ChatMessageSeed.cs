using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class ChatMessageSeed
    {
        public static readonly List<ChatMessage> Data = new()
        {
            new ChatMessage 
            { 
                Id = Guid.Parse("61b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), 
                ChatId = Guid.Parse("51b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), 
                SenderId = "8f3b2a1c-5d4e-4f3a-9b8c-7d6e5f4a3b2c", 
                ReceiverId = "7c9e6679-7425-40de-944b-e07fc1f90ae7",
                Text = "Hi, is the iPhone still available?", 
                SentAt = DateTime.UtcNow.AddMinutes(-10),
                IsRead = true,
                ReadAt = DateTime.UtcNow.AddMinutes(-5)
            },
            new ChatMessage 
            { 
                Id = Guid.Parse("62b2a2c4-5d4e-4f3a-9b8c-7d6e5f4a3b2c"), 
                ChatId = Guid.Parse("51b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), 
                SenderId = "7c9e6679-7425-40de-944b-e07fc1f90ae7", 
                ReceiverId = "8f3b2a1c-5d4e-4f3a-9b8c-7d6e5f4a3b2c",
                Text = "Yes, it is!", 
                SentAt = DateTime.UtcNow.AddMinutes(-2)
            }
        };
    }
}
