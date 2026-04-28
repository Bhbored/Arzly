using Arzly.Shared.Enums.Preference;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.UserPreference
{
    public class UserPreferenceAddRequest
    {
        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; } = string.Empty;

        public ThemeMode Theme { get; set; }
        public LanguageCode Language { get; set; }
        public bool PushNotifications { get; set; }
        public bool EmailNotifications { get; set; }
    }
}
