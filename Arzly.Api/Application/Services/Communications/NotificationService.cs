using Arzly.Api.Application.Contracts.Communications;
using Arzly.Api.Domain.Contracts.Communications;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.Notification;
using Arzly.Shared.DTOs.Response.Notification;
using Arzly.Shared.Enums.Notification;
using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Domain.Entities.Users;
using Arzly.Shared.Enums.Activity;

namespace Arzly.Api.Application.Services.Communications;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repository;
    private readonly IUserActivityLogRepository _activityLogs;

    public NotificationService(
        INotificationRepository repository,
        IUserActivityLogRepository activityLogs)
    {
        _repository = repository;
        _activityLogs = activityLogs;
    }

    public async Task<List<NotificationResponse>> GetInboxAsync(
        Guid userId, bool? isRead, int pageSize, int currentPage) =>
        (await _repository.GetInboxAsync(
            userId, isRead, Math.Clamp(pageSize, 1, 100), Math.Max(currentPage, 0)))
        .Select(x => x.ToResponse()).ToList();

    public async Task<NotificationResponse> MarkReadAsync(Guid id, Guid userId)
    {
        var notification = await _repository.MarkReadAsync(id, userId)
            ?? throw new UnauthorizedAccessException("The notification is not accessible to this user");
        return notification.ToResponse();
    }

    public async Task<NotificationResponse> SendTargetedAsync(NotificationAddRequest request, Guid actorId)
    {
        if (request.UserId is null || request.UserId == Guid.Empty)
            throw new ArgumentException("A target user is required");
        if (!await _repository.UserExistsAsync(request.UserId.Value))
            throw new ArgumentException("Target user not found");
        ValidateExpiry(request.ExpiresAt);

        var notification = request.ToEntity();
        notification.IsBroadcast = false;
        notification.IsRead = false;
        notification.ReadAt = null;
        notification.Source = NotificationSource.System;
        var saved = await _repository.AddAsync(notification);
        await AddAudit(actorId, saved.Id.ToString(), ActivityActionType.NotificationTargeted,
            $"Targeted notification sent to {saved.UserId}: {saved.Title}");
        return saved.ToResponse();
    }

    public async Task<int> BroadcastAsync(NotificationAddRequest request, Guid actorId)
    {
        ValidateExpiry(request.ExpiresAt);
        var template = request.ToEntity();
        template.UserId = null;
        template.IsBroadcast = true;
        template.IsRead = false;
        template.Source = NotificationSource.System;
        var delivered = await _repository.AddBroadcastAsync(template);
        await AddAudit(actorId, "broadcast", ActivityActionType.NotificationBroadcast,
            $"Broadcast delivered to {delivered} users: {template.Title}");
        return delivered;
    }

    private static void ValidateExpiry(DateTime? expiresAt)
    {
        if (expiresAt is not null && expiresAt <= DateTime.UtcNow)
            throw new ArgumentException("Notification expiration must be in the future");
    }

    private Task AddAudit(
        Guid actorId, string targetId, ActivityActionType actionType, string details) =>
        _activityLogs.AddAsync(new UserActivityLog
        {
            ActorId = actorId, ActorRole = "admin", ActionType = actionType,
            TargetType = ActivityTargetType.Notification, TargetId = targetId,
            Details = details, Timestamp = DateTime.UtcNow, IsSuccess = true
        });
}
