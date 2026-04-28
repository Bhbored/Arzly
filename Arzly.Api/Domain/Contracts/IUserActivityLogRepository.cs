using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Domain.Contracts
{
    public interface IUserActivityLogRepository : IBaseRepository<UserActivityLog, Guid>
    {
    }
}
