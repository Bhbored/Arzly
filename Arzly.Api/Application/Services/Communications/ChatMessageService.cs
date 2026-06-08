using Arzly.Api.Application.Contracts.Communications;
using Arzly.Api.Domain.Contracts.Communications;
using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Contracts.Communications;
using Arzly.Api.Domain.Entities.Communications;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.ChatMessage;
using Arzly.Shared.DTOs.Response.ChatMessage;

namespace Arzly.Api.Application.Services.Communications
{
    public class ChatMessageService : BaseService<ChatMessage, ChatMessageResponse, ChatMessageAddRequest, ChatMessageUpdateRequest, Guid>, IChatMessageService
    {
        public ChatMessageService(IChatMessageRepository repository) : base(repository)
        {
        }

        protected override ChatMessageResponse MapToDto(ChatMessage entity) => entity.ToResponse();
        protected override ChatMessage MapToEntity(ChatMessageAddRequest createDto) => createDto.ToEntity();
        protected override ChatMessage MapToEntity(ChatMessageUpdateRequest updateDto) => updateDto.ToEntity();
    }
}
