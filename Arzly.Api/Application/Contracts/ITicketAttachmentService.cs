using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.TicketAttachment;
using Arzly.Shared.DTOs.Response.TicketAttachment;

namespace Arzly.Api.Application.Contracts
{
    public interface ITicketAttachmentService : IBaseService<TicketAttachment, TicketAttachmentResponse, TicketAttachmentAddRequest, TicketAttachmentUpdateRequest, Guid>
    {
    }
}
