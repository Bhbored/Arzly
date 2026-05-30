using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Domain.Contracts.Support
{
    public interface ITicketRepository : IBaseRepository<Ticket, Guid>
    {
    }
}
