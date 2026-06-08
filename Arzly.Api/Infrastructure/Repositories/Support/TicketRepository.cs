using Arzly.Api.Domain.Contracts.Support;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Contracts.Support;
using Arzly.Api.Domain.Entities.Support;
using Arzly.Api.Infrastructure.Data.DataBaseContext;

namespace Arzly.Api.Infrastructure.Repositories.Support
{
    public class TicketRepository : BaseRepository<Ticket, Guid>, ITicketRepository
    {
        public TicketRepository(AppDbContext context) : base(context)
        {
        }
    }
}
