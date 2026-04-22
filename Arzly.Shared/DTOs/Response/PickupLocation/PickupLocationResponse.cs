using Arzly.Shared.Enums;

namespace Arzly.Shared.DTOs.Response.PickupLocation
{
    public class PickupLocationResponse
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public LocationLabel Label { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public bool IsDefault { get; set; }
    }
}
