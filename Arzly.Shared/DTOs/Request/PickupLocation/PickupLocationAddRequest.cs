using Arzly.Shared.Enums.Listing;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.PickupLocation
{
    public class PickupLocationAddRequest
    {
        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; } = string.Empty;

        public LocationLabel Label { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(500, ErrorMessage = "Address cannot exceed 500 characters.")]
        public string Address { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "Notes cannot exceed 200 characters.")]
        public string? Notes { get; set; }

        public double Lat { get; set; }
        public double Lon { get; set; }
        public bool IsDefault { get; set; }
    }
}
