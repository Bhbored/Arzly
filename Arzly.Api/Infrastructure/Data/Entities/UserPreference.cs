using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Infrastructure.Data.Entities
{
    public class UserPreference
    {
        [Key, ForeignKey(nameof(User))]
        public string UserId { get; set; } = "";
        public AppUser User { get; set; } = null!;
        public ThemeMode Theme { get; set; } = ThemeMode.System;
        public LanguageCode Language { get; set; } = LanguageCode.En;
        public bool PushNotifications { get; set; } = true;
        public bool EmailNotifications { get; set; } = false;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
