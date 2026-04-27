using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.Notification;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class NotificationSeed
    {
        public static readonly List<Notification> Data = new()
        {
            new Notification 
            { 
                Id = Guid.Parse("71b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), 
                UserId = "7c9e6679-7425-40de-944b-e07fc1f90ae7", 
                Title = "New Message", 
                Body = "You have a new message from User Two", 
                Source = NotificationSource.System, 
                ActionType = NotificationActionType.NewMessage,
                CreatedAt = DateTime.UtcNow
            }
        };
    }
}
