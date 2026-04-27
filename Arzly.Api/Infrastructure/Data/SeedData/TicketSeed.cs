using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.Ticket;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class TicketSeed
    {
        public static readonly List<Ticket> Data = new()
        {
            new Ticket 
            { 
                Id = Guid.Parse("81b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), 
                UserId = "7c9e6679-7425-40de-944b-e07fc1f90ae7", 
                Subject = "Technical Issue", 
                Status = TicketStatus.Open, 
                Priority = TicketPriority.High, 
                CreatedAt = DateTime.UtcNow
            }
        };
    }
}
