using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class TicketMessageSeed
    {
        public static readonly List<TicketMessage> Data = new()
        {
            new TicketMessage 
            { 
                Id = Guid.Parse("91b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), 
                TicketId = Guid.Parse("81b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), 
                SenderId = "7c9e6679-7425-40de-944b-e07fc1f90ae7", 
                ReceiverId = "8f3b2a1c-5d4e-4f3a-9b8c-7d6e5f4a3b2c",
                Message = "I am having trouble with the app", 
                SentAt = DateTime.UtcNow
            }
        };
    }
}
