using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.Chat;
using Arzly.Shared.DTOs.Response.Chat;

namespace Arzly.Api.Application.Services
{
    public class ChatService : BaseService<Chat, ChatResponse, ChatAddRequest, ChatUpdateRequest, Guid>, IChatService
    {
        public ChatService(IChatRepository repository) : base(repository)
        {
        }

        protected override ChatResponse MapToDto(Chat entity) => entity.ToResponse();
        protected override Chat MapToEntity(ChatAddRequest createDto) => createDto.ToEntity();
        protected override Chat MapToEntity(ChatUpdateRequest updateDto) => updateDto.ToEntity();
    }
}
