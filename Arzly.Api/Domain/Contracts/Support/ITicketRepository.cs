using Arzly.Api.Domain.Entities.Support;

namespace Arzly.Api.Domain.Contracts.Support
{
    public interface ITicketRepository : IBaseRepository<Ticket, Guid>
    {
    }
}
