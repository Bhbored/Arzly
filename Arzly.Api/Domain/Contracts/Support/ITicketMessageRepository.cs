using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Domain.Contracts.Support
{
    public interface ITicketMessageRepository : IBaseRepository<TicketMessage, Guid>
    {
    }
}
