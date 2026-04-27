using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.SavedListing
{
    public class SavedListingAddRequest
    {
        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Listing ID is required.")]
        public Guid ListingId { get; set; }
    }
}
