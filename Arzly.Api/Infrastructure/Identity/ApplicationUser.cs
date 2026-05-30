using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        // Authentication source
        public AuthMethod AuthMethod { get; set; } = AuthMethod.Email;

        [MaxLength(128)]
        public string? FirebaseUid { get; set; }

        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_.]+$", ErrorMessage = "Public name can only contain letters, numbers, spaces, hyphens, underscores, and periods.")]
        public string? PublicName { get; set; }

        [MaxLength(200)]
        public string? PublicLocation { get; set; }

        [Url]
        [MaxLength(2048)]
        public string? ProfileImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastActiveAt { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        public bool IsBanned { get; set; }

        [MaxLength(500)]
        public string? BanReason { get; set; }
        public DateTime? BanExpiresAt { get; set; }

        public bool IsVerified { get; set; } = false;

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirateDate { get; set; }

        // Navigation properties (inverse relationships)
        public virtual ICollection<PickupLocation>? DeliveryLocations { get; set; }
        public virtual ICollection<SavedListing>? SavedListings { get; set; }
        public virtual ICollection<SearchQuery>? SearchHistory { get; set; }
        public virtual UserPreference? Preferences { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
        public virtual ICollection<Chat>? ChatsInitiated { get; set; }
        public virtual ICollection<Chat>? ChatsReceived { get; set; }
        public virtual ICollection<UserActivityLog>? ActivityLogs { get; set; }
        public virtual ICollection<Ticket>? CreatedTickets { get; set; }
        public virtual ICollection<UserReport>? ReportsMade { get; set; }
        public virtual ICollection<UserReport>? ReportsReceived { get; set; }
        public virtual ICollection<Listing>? Listings { get; set; }
        public virtual ICollection<JobListing>? JobListings { get; set; }
    }
}
