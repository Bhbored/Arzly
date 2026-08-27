using Arzly.Api.Application.Contracts.Admin;
using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Response.UserActivityLog;
using Arzly.Shared.Enums.Activity;

namespace Arzly.Api.Application.Services.Admin;

public class AdminAuditService : IAdminAuditService
{
    private readonly IUserActivityLogRepository _repository;

    public AdminAuditService(IUserActivityLogRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UserActivityLogResponse>> GetAsync(
        ActivityActionType? actionType,
        ActivityTargetType? targetType,
        Guid? actorId,
        int pageSize,
        int currentPage) =>
        (await _repository.GetAllAsync(
            actionType, targetType, actorId,
            Math.Clamp(pageSize, 1, 100), Math.Max(currentPage, 0)))
        .Select(x => x.ToResponse()).ToList();
}
