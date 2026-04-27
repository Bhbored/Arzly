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
                Id = Guid.Parse("b1b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), 
                ActorId = "7c9e6679-7425-40de-944b-e07fc1f90ae7", 
                ActorRole = "User", 
                ActionType = ActivityActionType.Login, 
                IsSuccess = true, 
                Timestamp = DateTime.UtcNow
            }
        };
    }
}
