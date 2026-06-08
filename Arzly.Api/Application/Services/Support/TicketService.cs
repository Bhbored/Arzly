using Arzly.Api.Application.Contracts.Support;
using Arzly.Api.Domain.Contracts.Support;
using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Contracts.Support;
using Arzly.Api.Domain.Entities.Support;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.Ticket;
using Arzly.Shared.DTOs.Response.Ticket;

namespace Arzly.Api.Application.Services.Support
{
    public class TicketService : BaseService<Ticket, TicketResponse, TicketAddRequest, TicketUpdateRequest, Guid>, ITicketService
    {
        public TicketService(ITicketRepository repository) : base(repository)
        {
        }

        protected override TicketResponse MapToDto(Ticket entity) => entity.ToResponse();
        protected override Ticket MapToEntity(TicketAddRequest createDto) => createDto.ToEntity();
        protected override Ticket MapToEntity(TicketUpdateRequest updateDto) => updateDto.ToEntity();
    }
}
