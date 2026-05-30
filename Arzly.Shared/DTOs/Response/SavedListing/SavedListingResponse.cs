namespace Arzly.Shared.DTOs.Response.SavedListing
{
    public class SavedListingResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ListingId { get; set; }
        public DateTime SavedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
