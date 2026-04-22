namespace Arzly.Shared.DTOs.Request.SavedListing
{
    public class SavedListingUpdateRequest
    {
        public Guid Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
