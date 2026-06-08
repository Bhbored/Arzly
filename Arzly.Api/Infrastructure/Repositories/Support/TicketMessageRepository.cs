using Arzly.Api.Domain.Contracts.Support;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Contracts.Support;
using Arzly.Api.Domain.Entities.Support;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories.Support
{
    public class TicketMessageRepository : BaseRepository<TicketMessage, Guid>, ITicketMessageRepository
    {
        public TicketMessageRepository(DbContext context) : base(context)
        {
        }
    }
}
