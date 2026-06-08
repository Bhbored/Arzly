using Arzly.Api.Domain.Entities.Communications;
using Arzly.Shared.DTOs.Request.ChatMessage;
using Arzly.Shared.DTOs.Response.ChatMessage;

namespace Arzly.Api.Application.Contracts.Communications
{
    public interface IChatMessageService : IBaseService<ChatMessage, ChatMessageResponse, ChatMessageAddRequest, ChatMessageUpdateRequest, Guid>
    {
    }
}
