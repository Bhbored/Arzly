using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class TicketMessageRepository : BaseRepository<TicketMessage, Guid>, ITicketMessageRepository
    {
        public TicketMessageRepository(DbContext context) : base(context)
        {
        }
    }
}
