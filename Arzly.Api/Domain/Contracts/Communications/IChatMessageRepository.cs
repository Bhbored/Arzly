using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Domain.Contracts.Communications
{
    public interface IChatMessageRepository : IBaseRepository<ChatMessage, Guid>
    {
    }
}
