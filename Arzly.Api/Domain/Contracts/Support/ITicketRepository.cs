using Arzly.Api.Domain.Entities.Support;

namespace Arzly.Api.Domain.Contracts.Support
{
    public interface ITicketRepository : IBaseRepository<Ticket, Guid>
    {
        Task<List<Ticket>> GetUserTicketsAsync(Guid userId, int pageSize, int currentPage);
        Task<List<Ticket>> GetQueueAsync(int pageSize, int currentPage);
        Task<Ticket?> GetConversationAsync(Guid id);
        Task<Ticket?> UpdateStatusAsync(Guid id, Arzly.Shared.Enums.Ticket.TicketStatus status, Guid? assignedToId);
        Task<TicketMessage> AddMessageAsync(TicketMessage message);
        Task<TicketAttachment> AddAttachmentAsync(TicketAttachment attachment);
    }
}
