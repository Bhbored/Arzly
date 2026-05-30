using Arzly.Shared.Enums.Preference;

namespace Arzly.Shared.DTOs.Response.UserPreference
{
    public class UserPreferenceResponse
    {
        public Guid UserId { get; set; }
        public ThemeMode Theme { get; set; }
        public LanguageCode Language { get; set; }
        public bool PushNotifications { get; set; }
        public bool EmailNotifications { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
