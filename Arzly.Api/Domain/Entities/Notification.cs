using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums.Notification;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities
{
    [NotMapped]
    public class Notification
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? UserId { get; set; }

        public Guid? ChatterId {  get; set; }

        public Guid? ListingId {  get; set; }

        [ForeignKey(nameof(ChatterId))]
        public virtual AppUser? Chatter { get; set; }
        [ForeignKey(nameof(ListingId))]
        public virtual Listing? Listing { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual AppUser? User { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Body is required.")]
        [MaxLength(1000, ErrorMessage = "Body cannot exceed 1000 characters.")]
        public string Body { get; set; } = string.Empty;

        public NotificationSource Source { get; set; }

        public bool IsBroadcast { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ReadAt { get; set; }

        [MaxLength(500, ErrorMessage = "Deep link cannot exceed 500 characters.")]
        public string? DeepLink { get; set; }

        public NotificationActionType ActionType { get; set; }

        public string? Metadata { get; set; } // JSON for extra data

        public DateTime? ExpiresAt { get; set; } // Auto-cleanup
    }
}
