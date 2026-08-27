using Arzly.Api.Domain.Entities.Users;

namespace Arzly.Api.Domain.Contracts.Users
{
    public interface IUserActivityLogRepository 
    {
        Task AddAsync(UserActivityLog activity);
        Task<List<UserActivityLog>> GetByTargetAsync(
            Arzly.Shared.Enums.Activity.ActivityTargetType targetType,
            string targetId,
            int pageSize,
            int currentPage);
        Task<List<UserActivityLog>> GetAllAsync(
            Arzly.Shared.Enums.Activity.ActivityActionType? actionType,
            Arzly.Shared.Enums.Activity.ActivityTargetType? targetType,
            Guid? actorId,
            int pageSize,
            int currentPage);
    }
}
