using Arzly.Api.Domain.Entities.Support;
using Arzly.Shared.DTOs.Request.TicketAttachment;
using Arzly.Shared.DTOs.Response.TicketAttachment;

namespace Arzly.Api.Application.Contracts.Support
{
    public interface ITicketAttachmentService
    {
        Task<List<TicketAttachmentResponse>> GetAllAsync();
        Task<TicketAttachmentResponse?> GetByIdAsync(Guid id);
        Task<TicketAttachmentResponse?> CreateAsync(TicketAttachmentAddRequest? request, Guid userId);
        Task<TicketAttachmentResponse?> UpdateAsync(TicketAttachmentUpdateRequest? request, Guid userId);
        Task<bool> DeleteAsync(Guid id);
    }
}
