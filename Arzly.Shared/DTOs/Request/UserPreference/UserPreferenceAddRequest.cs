using Arzly.Shared.Enums.Preference;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.UserPreference
{
    public class UserPreferenceAddRequest
    {
        [Required(ErrorMessage = "User ID is required.")]
        public Guid UserId { get; set; }

        public ThemeMode Theme { get; set; }
        public LanguageCode Language { get; set; }
        public bool PushNotifications { get; set; }
        public bool EmailNotifications { get; set; }
    }
}
