using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class TicketAttachmentRepository : BaseRepository<TicketAttachment, Guid>, ITicketAttachmentRepository
    {
        public TicketAttachmentRepository(DbContext context) : base(context)
        {
        }
    }
}
