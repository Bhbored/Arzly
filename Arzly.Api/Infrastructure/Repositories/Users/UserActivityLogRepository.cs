using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities.Users;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Shared.Enums.Activity;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories.Users
{
    public class UserActivityLogRepository :  IUserActivityLogRepository
    {
        private readonly AppDbContext _db;

        public UserActivityLogRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(UserActivityLog activity)
        {
            _db.UserActivityLogs.Add(activity);
            await _db.SaveChangesAsync();
        }

        public Task<List<UserActivityLog>> GetByTargetAsync(
            ActivityTargetType targetType,
            string targetId,
            int pageSize,
            int currentPage) => _db.UserActivityLogs.AsNoTracking()
            .Where(x => x.TargetType == targetType && x.TargetId == targetId)
            .OrderByDescending(x => x.Timestamp)
            .Skip(currentPage * pageSize).Take(pageSize).ToListAsync();

        public Task<List<UserActivityLog>> GetAllAsync(
            ActivityActionType? actionType,
            ActivityTargetType? targetType,
            Guid? actorId,
            int pageSize,
            int currentPage)
        {
            var query = _db.UserActivityLogs.IgnoreQueryFilters().AsNoTracking();
            if (actionType is not null) query = query.Where(x => x.ActionType == actionType);
            if (targetType is not null) query = query.Where(x => x.TargetType == targetType);
            if (actorId is not null) query = query.Where(x => x.ActorId == actorId);
            return query.OrderByDescending(x => x.Timestamp)
                .Skip(currentPage * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
