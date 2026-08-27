using Arzly.Api.Domain.Entities.Communications;

namespace Arzly.Api.Domain.Contracts.Communications;

public interface INotificationRepository
{
    Task<List<Notification>> GetInboxAsync(Guid userId, bool? isRead, int pageSize, int currentPage);
    Task<Notification?> MarkReadAsync(Guid id, Guid userId);
    Task<Notification> AddAsync(Notification notification);
    Task<int> AddBroadcastAsync(Notification template);
    Task<bool> UserExistsAsync(Guid userId);
}
