using Arzly.Api.Domain.Entities.Communications;

namespace Arzly.Api.Domain.Contracts.Communications
{
    public interface IChatMessageRepository : IBaseRepository<ChatMessage, Guid>
    {
    }
}
