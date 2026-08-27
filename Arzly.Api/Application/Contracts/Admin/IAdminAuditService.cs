using Arzly.Shared.DTOs.Response.UserActivityLog;
using Arzly.Shared.Enums.Activity;

namespace Arzly.Api.Application.Contracts.Admin;

public interface IAdminAuditService
{
    Task<List<UserActivityLogResponse>> GetAsync(
        ActivityActionType? actionType,
        ActivityTargetType? targetType,
        Guid? actorId,
        int pageSize,
        int currentPage);
}
