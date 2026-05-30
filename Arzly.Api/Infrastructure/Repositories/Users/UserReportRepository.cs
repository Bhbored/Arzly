using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Infrastructure.Data.DataBaseContext;

namespace Arzly.Api.Infrastructure.Repositories.Users
{
    public class UserReportRepository : BaseRepository<UserReport, Guid>, IUserReportRepository
    {
        public UserReportRepository(AppDbContext context) : base(context)
        {
        }
    }
}
