using Arzly.Shared.DTOs.Request.Notification;
using Arzly.Shared.DTOs.Response.Notification;

namespace Arzly.Api.Application.Contracts.Communications;

public interface INotificationService
{
    Task<List<NotificationResponse>> GetInboxAsync(Guid userId, bool? isRead, int pageSize, int currentPage);
    Task<NotificationResponse> MarkReadAsync(Guid id, Guid userId);
    Task<NotificationResponse> SendTargetedAsync(NotificationAddRequest request, Guid actorId);
    Task<int> BroadcastAsync(NotificationAddRequest request, Guid actorId);
}
