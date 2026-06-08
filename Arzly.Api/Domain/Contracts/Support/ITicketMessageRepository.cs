using Arzly.Api.Domain.Entities.Support;

namespace Arzly.Api.Domain.Contracts.Support
{
    public interface ITicketMessageRepository : IBaseRepository<TicketMessage, Guid>
    {
    }
}
