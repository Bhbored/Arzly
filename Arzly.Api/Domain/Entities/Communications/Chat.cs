using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities.Communications
{
    public class Chat
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public ChatRole ContextRole { get; set; }

        public bool IsArchived { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public bool IsDiscontinued { get; set; } = false;

        public DateTime LastActivity { get; set; } = DateTime.UtcNow;

        // Foreign keys
        [Required(ErrorMessage = "Initiator ID is required.")]
        public Guid InitiatorId { get; set; }

        [Required(ErrorMessage = "Receiver ID is required.")]
        public Guid ReceiverId { get; set; }

        public Guid? ListingId { get; set; }
        public Guid? JobListingId { get; set; }

        // Navigation
        [ForeignKey(nameof(InitiatorId))]
        public virtual ApplicationUser Initiator { get; set; } = null!;

        [ForeignKey(nameof(ReceiverId))]
        public virtual ApplicationUser Receiver { get; set; } = null!;

        [ForeignKey(nameof(ListingId))]
        public virtual Listing? Listing { get; set; }

        [ForeignKey(nameof(JobListingId))]
        public virtual JobListing? JobListing { get; set; }

        public virtual ICollection<ChatMessage>? Messages { get; set; }
    }
}
