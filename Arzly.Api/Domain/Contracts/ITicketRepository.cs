using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Domain.Contracts
{
    public interface ITicketRepository : IBaseRepository<Ticket, Guid>
    {
    }
}
