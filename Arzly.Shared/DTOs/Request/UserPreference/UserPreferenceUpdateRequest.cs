using Arzly.Shared.Enums;

namespace Arzly.Shared.DTOs.Request.UserPreference
{
    public class UserPreferenceUpdateRequest
    {
        public string UserId { get; set; } = string.Empty;
        public ThemeMode Theme { get; set; }
        public LanguageCode Language { get; set; }
        public bool PushNotifications { get; set; }
        public bool EmailNotifications { get; set; }
    }
}
