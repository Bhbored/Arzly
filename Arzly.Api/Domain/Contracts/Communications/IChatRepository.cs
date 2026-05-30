using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Domain.Contracts.Communications
{
    public interface IChatRepository : IBaseRepository<Chat, Guid>
    {
    }
}
