using Arzly.Api.Domain.Entities.Support;
using Arzly.Shared.DTOs.Request.Ticket;
using Arzly.Shared.DTOs.Response.Ticket;

namespace Arzly.Api.Application.Contracts.Support
{
    public interface ITicketService : IBaseService<Ticket, TicketResponse, TicketAddRequest, TicketUpdateRequest, Guid>
    {
        Task<List<TicketResponse>> GetUserTicketsAsync(Guid userId, int pageSize, int currentPage);
        Task<List<TicketResponse>> GetQueueAsync(int pageSize, int currentPage);
        Task<TicketConversationResponse> GetConversationAsync(Guid id, Guid userId, bool isStaff);
        Task<TicketResponse> SetStatusAsync(Guid id, Guid actorId, Arzly.Shared.Enums.Ticket.TicketStatus status);
        Task<Arzly.Shared.DTOs.Response.TicketMessage.TicketMessageResponse> AddMessageAsync(
            Arzly.Shared.DTOs.Request.TicketMessage.TicketMessageAddRequest request, Guid userId, bool isStaff);
        Task<Arzly.Shared.DTOs.Response.TicketAttachment.TicketAttachmentResponse> AddAttachmentAsync(
            Arzly.Shared.DTOs.Request.TicketAttachment.TicketAttachmentAddRequest request, Guid userId, bool isStaff);
    }
}
