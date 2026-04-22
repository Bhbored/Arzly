namespace Arzly.Shared.DTOs.Response.SavedListing
{
    public class SavedListingResponse
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Guid ListingId { get; set; }
        public DateTime SavedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
