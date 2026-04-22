namespace Arzly.Shared.DTOs.Request.SavedListing
{
    public class SavedListingAddRequest
    {
        public string UserId { get; set; } = string.Empty;
        public Guid ListingId { get; set; }
    }
}
