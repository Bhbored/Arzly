using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.Preference;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class UserPreferenceSeed
    {
        public static readonly List<UserPreference> Data = new()
        {
            new UserPreference 
            { 
                UserId = "7c9e6679-7425-40de-944b-e07fc1f90ae7", 
                Theme = ThemeMode.Dark, 
                Language = LanguageCode.En, 
                PushNotifications = true, 
                EmailNotifications = true, 
                UpdatedAt = DateTime.UtcNow
            },
            new UserPreference 
            { 
                UserId = "8f3b2a1c-5d4e-4f3a-9b8c-7d6e5f4a3b2c", 
                Theme = ThemeMode.Light, 
                Language = LanguageCode.Ar, 
                PushNotifications = false, 
                EmailNotifications = false, 
                UpdatedAt = DateTime.UtcNow
            }
        };
    }
}
