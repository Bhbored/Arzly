using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Domain.Contracts
{
    public interface IChatMessageRepository : IBaseRepository<ChatMessage, Guid>
    {
    }
}
