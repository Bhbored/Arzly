using Arzly.Api.Infrastructure.Identity;

namespace Arzly.Api.Infrastructure.Data.Entities
{
    public class SavedListing
    {
        public string UserId { get; set; } = "";
        public Guid ListingId { get; set; }
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
        public AppUser User { get; set; } = null!;
        public Listing Listing { get; set; } = null!;
    }
}
