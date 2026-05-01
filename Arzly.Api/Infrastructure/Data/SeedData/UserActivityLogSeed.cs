using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.Activity;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class UserActivityLogSeed
    {
        public static readonly List<UserActivityLog> Data = new()
        {
            new UserActivityLog
            {
                Id = Guid.NewGuid(),
                ActorId = "user-1-id",
                ActorRole = "User",
                ActionType = ActivityActionType.Register,
                TargetType = ActivityTargetType.User,
                TargetId = "user-1-id",
                IPAddress = "192.168.1.100",
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)",
                Details = "User registered successfully",
                Timestamp = DateTime.UtcNow.AddDays(-30),
                IsSuccess = true
            },
            new UserActivityLog
            {
                Id = Guid.NewGuid(),
                ActorId = "user-1-id",
                ActorRole = "User",
                ActionType = ActivityActionType.Login,
                TargetType = ActivityTargetType.None,
                IPAddress = "192.168.1.100",
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)",
                Timestamp = DateTime.UtcNow.AddDays(-1),
                IsSuccess = true,
                DurationMs = 250
            },
            new UserActivityLog
            {
                Id = Guid.NewGuid(),
                ActorId = "user-1-id",
                ActorRole = "User",
                ActionType = ActivityActionType.ListingCreated,
                TargetType = ActivityTargetType.Listing,
                TargetId = "listing-1-id",
                IPAddress = "192.168.1.100",
                Details = "Created listing: BMW 320i",
                Timestamp = DateTime.UtcNow.AddDays(-10),
                IsSuccess = true
            },
            new UserActivityLog
            {
                Id = Guid.NewGuid(),
                ActorId = "user-2-id",
                ActorRole = "User",
                ActionType = ActivityActionType.Register,
                TargetType = ActivityTargetType.User,
                TargetId = "user-2-id",
                IPAddress = "192.168.1.105",
                UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7)",
                Details = "User registered successfully",
                Timestamp = DateTime.UtcNow.AddDays(-20),
                IsSuccess = true
            },
            new UserActivityLog
            {
                Id = Guid.NewGuid(),
                ActorId = "user-2-id",
                ActorRole = "User",
                ActionType = ActivityActionType.MessageSent,
                TargetType = ActivityTargetType.Chat,
                TargetId = "chat-1-id",
                IPAddress = "192.168.1.105",
                Details = "Sent message in chat",
                Timestamp = DateTime.UtcNow.AddHours(-5),
                IsSuccess = true,
                DurationMs = 120
            },
            new UserActivityLog
            {
                Id = Guid.NewGuid(),
                ActorId = "user-1-id",
                ActorRole = "User",
                ActionType = ActivityActionType.ListingSaved,
                TargetType = ActivityTargetType.Listing,
                TargetId = "listing-2-id",
                IPAddress = "192.168.1.100",
                Details = "Saved listing to favorites",
                Timestamp = DateTime.UtcNow.AddHours(-2),
                IsSuccess = true
            }
        };
    }
}
