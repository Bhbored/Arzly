using Arzly.Api.Domain.Entities.Users;

namespace Arzly.Api.Domain.Contracts.Users
{
    public interface IUserReportRepository : IBaseRepository<UserReport, Guid>
    {
    }
}
