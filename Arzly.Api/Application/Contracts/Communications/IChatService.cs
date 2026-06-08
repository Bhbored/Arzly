using Arzly.Api.Domain.Entities.Communications;
using Arzly.Shared.DTOs.Request.Chat;
using Arzly.Shared.DTOs.Response.Chat;

namespace Arzly.Api.Application.Contracts.Communications
{
    public interface IChatService : IBaseService<Chat, ChatResponse, ChatAddRequest, ChatUpdateRequest, Guid>
    {
    }
}
