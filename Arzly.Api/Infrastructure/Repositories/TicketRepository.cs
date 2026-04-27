using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Infrastructure.Data.DataBaseContext;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class TicketRepository : BaseRepository<Ticket, Guid>, ITicketRepository
    {
        public TicketRepository(AppDbContext context) : base(context)
        {
        }
    }
}
