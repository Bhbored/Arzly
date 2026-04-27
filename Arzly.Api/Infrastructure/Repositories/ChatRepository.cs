using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Infrastructure.Data.DataBaseContext;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class ChatRepository : BaseRepository<Chat, Guid>, IChatRepository
    {
        public ChatRepository(AppDbContext context) : base(context)
        {
        }
    }
}
