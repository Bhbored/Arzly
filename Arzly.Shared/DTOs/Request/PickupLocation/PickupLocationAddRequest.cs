using Arzly.Shared.Enums.Listing;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.PickupLocation
{
    public class PickupLocationAddRequest
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        public LocationLabel Label { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Notes { get; set; }

        public double Lat { get; set; }
        public double Lon { get; set; }
        public bool IsDefault { get; set; }
    }
}
