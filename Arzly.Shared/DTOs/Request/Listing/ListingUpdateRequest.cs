using Arzly.Shared.Enums;

namespace Arzly.Shared.DTOs.Request.Listing
{
    public class ListingUpdateRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public ListingStatus Status { get; set; }
        public string? PrimaryImageUrl { get; set; }
        public List<string>? ImagesUrl { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubcategoryId { get; set; }
        public Guid? SubcategoryOptionsId { get; set; }
        public Guid PickupLocationId { get; set; }
    }
}
