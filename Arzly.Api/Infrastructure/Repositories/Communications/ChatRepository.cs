using Arzly.Api.Domain.Contracts.Communications;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Contracts.Communications;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Infrastructure.Data.DataBaseContext;

namespace Arzly.Api.Infrastructure.Repositories.Communications
{
    public class ChatRepository : BaseRepository<Chat, Guid>, IChatRepository
    {
        public ChatRepository(AppDbContext context) : base(context)
        {
        }
    }
}
