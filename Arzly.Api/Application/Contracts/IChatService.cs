using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.Chat;
using Arzly.Shared.DTOs.Response.Chat;

namespace Arzly.Api.Application.Contracts
{
    public interface IChatService : IBaseService<Chat, ChatResponse, ChatAddRequest, ChatUpdateRequest, Guid>
    {
    }
}
