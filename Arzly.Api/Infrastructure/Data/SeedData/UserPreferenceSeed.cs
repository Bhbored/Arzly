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
                UserId = "user-1-id",
                Theme = ThemeMode.Dark,
                Language = LanguageCode.En,
                PushNotifications = true,
                EmailNotifications = false,
                UpdatedAt = DateTime.UtcNow
            },
            new UserPreference
            {
                UserId = "user-2-id",
                Theme = ThemeMode.Light,
                Language = LanguageCode.En,
                PushNotifications = true,
                EmailNotifications = true,
                UpdatedAt = DateTime.UtcNow
            }
        };
    }
}
