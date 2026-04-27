using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.SavedListing
{
    public class SavedListingAddRequest
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public Guid ListingId { get; set; }
    }
}
