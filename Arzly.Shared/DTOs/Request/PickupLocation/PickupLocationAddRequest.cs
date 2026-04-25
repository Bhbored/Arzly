using Arzly.Shared.Enums.Listing;

namespace Arzly.Shared.DTOs.Request.PickupLocation
{
    public class PickupLocationAddRequest
    {
        public string UserId { get; set; } = string.Empty;
        public LocationLabel Label { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public bool IsDefault { get; set; }
    }
}
