using Arzly.Api.Infrastructure.Data.Entities;
using Arzly.Shared.Enums;
using Microsoft.AspNetCore.Identity;

namespace Arzly.Api.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public AuthMethod AuthMethod { get; set; } = AuthMethod.Firebase;// Default for mobile
        public string? FirebaseUid { get; set; } // Links mobile Firebase auth to Identity
        public string? PublicName { get; set; }
        public string? PublicLocation { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public virtual ICollection<DeliveryLocation>? DeliveryLocations { get; set; } 
        public virtual ICollection<SavedListing>? SavedListings { get; set; }
        public virtual ICollection<SearchQuery>? SearchHistory { get; set; }
        public virtual UserPreference? Preferences { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
        public virtual ICollection<Chat>? InitiatedChats { get; set; }
        public virtual ICollection<Chat>? ReceivedChats { get; set; }
        public virtual ICollection<ChatMessage>? SentMessages { get; set; }
        public virtual ICollection<UserActivityLog>? ActivityLogs { get; set; }
    }
}
