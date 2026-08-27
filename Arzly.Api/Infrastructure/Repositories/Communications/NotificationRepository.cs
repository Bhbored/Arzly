using Arzly.Api.Domain.Contracts.Communications;
using Arzly.Api.Domain.Entities.Communications;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories.Communications;

public class NotificationRepository : INotificationRepository
{
    private readonly AppDbContext _db;

    public NotificationRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<List<Notification>> GetInboxAsync(
        Guid userId, bool? isRead, int pageSize, int currentPage)
    {
        var query = _db.Notifications.AsNoTracking().Where(x => x.UserId == userId)
            .Where(x => x.ExpiresAt == null || x.ExpiresAt > DateTime.UtcNow);
        if (isRead is not null)
            query = query.Where(x => x.IsRead == isRead);
        return query.OrderByDescending(x => x.CreatedAt)
            .Skip(currentPage * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<Notification?> MarkReadAsync(Guid id, Guid userId)
    {
        var notification = await _db.Notifications.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (notification is null) return null;
        notification.IsRead = true;
        notification.ReadAt ??= DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return notification;
    }

    public async Task<Notification> AddAsync(Notification notification)
    {
        _db.Notifications.Add(notification);
        await _db.SaveChangesAsync();
        return notification;
    }

    public async Task<int> AddBroadcastAsync(Notification template)
    {
        var userIds = await _db.Users.AsNoTracking().Select(x => x.Id).ToListAsync();
        var notifications = userIds.Select(userId => new Notification
        {
            Id = Guid.NewGuid(), UserId = userId, Title = template.Title, Body = template.Body,
            Source = template.Source, IsBroadcast = true, DeepLink = template.DeepLink,
            ActionType = template.ActionType, Metadata = template.Metadata,
            ExpiresAt = template.ExpiresAt, CreatedAt = DateTime.UtcNow
        }).ToList();
        _db.Notifications.AddRange(notifications);
        await _db.SaveChangesAsync();
        return notifications.Count;
    }

    public Task<bool> UserExistsAsync(Guid userId) => _db.Users.AnyAsync(x => x.Id == userId);
}
