using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class ChatSeed
    {
        public static readonly List<Chat> Data = new()
        {
            new Chat 
            { 
                Id = Guid.Parse("51b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), 
                InitiatorId = "8f3b2a1c-5d4e-4f3a-9b8c-7d6e5f4a3b2c", 
                ReceiverId = "7c9e6679-7425-40de-944b-e07fc1f90ae7", 
                ListingId = Guid.Parse("11b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"),
                ContextRole = ChatRole.Buyer,
                LastActivity = DateTime.UtcNow
            }
        };
    }
}
