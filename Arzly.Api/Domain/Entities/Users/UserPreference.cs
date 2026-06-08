using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums.Preference;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities.Users
{
    public class UserPreference
    {
        [Key, ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public ThemeMode Theme { get; set; } = ThemeMode.System;
        public LanguageCode Language { get; set; } = LanguageCode.En;

        public bool PushNotifications { get; set; } = true;
        public bool EmailNotifications { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
