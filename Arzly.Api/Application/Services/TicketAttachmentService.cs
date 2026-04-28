using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.TicketAttachment;
using Arzly.Shared.DTOs.Response.TicketAttachment;

namespace Arzly.Api.Application.Services
{
    public class TicketAttachmentService : BaseService<TicketAttachment, TicketAttachmentResponse, TicketAttachmentAddRequest, TicketAttachmentUpdateRequest, Guid>, ITicketAttachmentService
    {
        public TicketAttachmentService(ITicketAttachmentRepository repository) : base(repository)
        {
        }

        protected override TicketAttachmentResponse MapToDto(TicketAttachment entity) => entity.ToResponse();
        protected override TicketAttachment MapToEntity(TicketAttachmentAddRequest createDto) => createDto.ToEntity();
        protected override TicketAttachment MapToEntity(TicketAttachmentUpdateRequest updateDto) => updateDto.ToEntity();
    }
}
