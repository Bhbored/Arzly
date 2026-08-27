using Arzly.Api.Domain.Contracts.Support;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities.Support;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Shared.Enums.Ticket;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories.Support
{
    public class TicketRepository : BaseRepository<Ticket, Guid>, ITicketRepository
    {
        private readonly AppDbContext _db;
        public TicketRepository(AppDbContext context) : base(context)
        {
            _db = context;
        }

        public Task<List<Ticket>> GetUserTicketsAsync(Guid userId, int pageSize, int currentPage) =>
            _db.Tickets.AsNoTracking().Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt).Skip(currentPage * pageSize).Take(pageSize).ToListAsync();

        public Task<List<Ticket>> GetQueueAsync(int pageSize, int currentPage) =>
            _db.Tickets.AsNoTracking().OrderByDescending(x => x.CreatedAt)
                .Skip(currentPage * pageSize).Take(pageSize).ToListAsync();

        public Task<Ticket?> GetConversationAsync(Guid id) => _db.Tickets
            .Include(x => x.Messages!.OrderBy(message => message.SentAt))
            .Include(x => x.Attachments!.OrderBy(attachment => attachment.UploadedAt))
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Ticket?> UpdateStatusAsync(Guid id, TicketStatus status, Guid? assignedToId)
        {
            var ticket = await _db.Tickets.FirstOrDefaultAsync(x => x.Id == id);
            if (ticket is null) return null;
            ticket.Status = status;
            ticket.AssignedToId ??= assignedToId;
            ticket.LastUpdatedAt = DateTime.UtcNow;
            ticket.ClosedAt = status is TicketStatus.Resolved or TicketStatus.Closed ? DateTime.UtcNow : null;
            await _db.SaveChangesAsync();
            return ticket;
        }

        public async Task<TicketMessage> AddMessageAsync(TicketMessage message)
        {
            _db.TicketMessages.Add(message);
            var ticket = await _db.Tickets.SingleAsync(x => x.Id == message.TicketId);
            ticket.LastUpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return message;
        }

        public async Task<TicketAttachment> AddAttachmentAsync(TicketAttachment attachment)
        {
            _db.TicketAttachments.Add(attachment);
            await _db.SaveChangesAsync();
            return attachment;
        }
    }
}
