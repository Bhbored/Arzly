using Arzly.Shared.Enums.Listing;

namespace Arzly.Shared.DTOs.Request.Listing
{
    public class ListingAddRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public ListingStatus Status { get; set; }
        public string? PrimaryImageUrl { get; set; }
        public List<string>? ImagesUrl { get; set; }
        public string OwnerId { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public Guid SubcategoryId { get; set; }
        public Guid? SubcategoryOptionsId { get; set; }
        public Guid PickupLocationId { get; set; }
    }
}
