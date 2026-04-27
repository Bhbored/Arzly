using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.Ticket;
using Arzly.Shared.DTOs.Response.Ticket;

namespace Arzly.Api.Application.Contracts
{
    public interface ITicketService : IBaseService<Ticket, TicketResponse, TicketAddRequest, TicketUpdateRequest, Guid>
    {
    }
}
