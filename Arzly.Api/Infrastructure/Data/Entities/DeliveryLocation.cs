using Arzly.Api.Infrastructure.Identity;

namespace Arzly.Api.Infrastructure.Data.Entities
{
    public class DeliveryLocation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = "";
        public AppUser User { get; set; } = null!;
        public string Label { get; set; } = ""; // "Home", "Work"
        public string Address { get; set; } = "";
        public string? Notes { get; set; }
        public bool IsDefault { get; set; }
    }
}
