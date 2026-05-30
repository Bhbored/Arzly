using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.SavedListing
{
    public class SavedListingAddRequest
    {
        [Required(ErrorMessage = "User ID is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Listing ID is required.")]
        public Guid ListingId { get; set; }
    }
}
