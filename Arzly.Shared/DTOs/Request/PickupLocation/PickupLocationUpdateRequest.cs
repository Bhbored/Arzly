using Arzly.Shared.Enums;

namespace Arzly.Shared.DTOs.Request.PickupLocation
{
    public class PickupLocationUpdateRequest
    {
        public Guid Id { get; set; }
        public LocationLabel Label { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public bool IsDefault { get; set; }
    }
}
