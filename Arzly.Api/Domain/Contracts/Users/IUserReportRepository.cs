using Arzly.Api.Domain.Entities.Users;

namespace Arzly.Api.Domain.Contracts.Users
{
    public interface IUserReportRepository : IBaseRepository<UserReport, Guid>
    {
        Task<UserReport?> GetByIdForUserAsync(Guid id, Guid userId, bool canModerate);
        Task<UserReport?> ResolveAsync(Guid id, Guid resolverId, bool isResolved);
    }
}
