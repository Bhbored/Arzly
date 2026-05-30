using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.TicketMessage;
using Arzly.Shared.DTOs.Response.TicketMessage;

namespace Arzly.Api.Application.Contracts.Support
{
    public interface ITicketMessageService : IBaseService<TicketMessage, TicketMessageResponse, TicketMessageAddRequest, TicketMessageUpdateRequest, Guid>
    {
    }
}
