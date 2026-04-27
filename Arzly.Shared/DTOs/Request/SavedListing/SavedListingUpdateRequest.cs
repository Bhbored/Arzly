using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.SavedListing
{
    public class SavedListingUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
