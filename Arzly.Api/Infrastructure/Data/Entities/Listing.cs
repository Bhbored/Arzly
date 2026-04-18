using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums;

namespace Arzly.Api.Infrastructure.Data.Entities
{
    public class Listing
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string OwnerId { get; set; } = "";
        public AppUser Owner { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string? PriceOrType { get; set; } // e.g., "Free", "20$", "Swap"
        public ListingStatus Status { get; set; } = ListingStatus.Pending;
        public string? PrimaryImageUrl { get; set; }
        public string LocationHint { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<Chat>? RelatedChats { get; set; } 
    }
}
