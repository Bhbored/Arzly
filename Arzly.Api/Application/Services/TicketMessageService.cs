using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.TicketMessage;
using Arzly.Shared.DTOs.Response.TicketMessage;

namespace Arzly.Api.Application.Services
{
    public class TicketMessageService : BaseService<TicketMessage, TicketMessageResponse, TicketMessageAddRequest, TicketMessageUpdateRequest, Guid>, ITicketMessageService
    {
        public TicketMessageService(ITicketMessageRepository repository) : base(repository)
        {
        }

        protected override TicketMessageResponse MapToDto(TicketMessage entity) => entity.ToResponse();
        protected override TicketMessage MapToEntity(TicketMessageAddRequest createDto) => createDto.ToEntity();
        protected override TicketMessage MapToEntity(TicketMessageUpdateRequest updateDto) => updateDto.ToEntity();
    }
}
