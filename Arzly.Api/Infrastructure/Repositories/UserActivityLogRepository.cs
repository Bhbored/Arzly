using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class UserActivityLogRepository : BaseRepository<UserActivityLog, Guid>, IUserActivityLogRepository
    {
        public UserActivityLogRepository(DbContext context) : base(context)
        {
        }
    }
}
