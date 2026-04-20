using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities
{
    public class PickupLocation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public virtual AppUser User { get; set; } = null!;

        public LocationLabel Label { get; set; } = LocationLabel.Home;

        [Required]
        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Notes { get; set; }

        public Coordinate? Coordinates { get; set; }

        public bool IsDefault { get; set; }

        public virtual ICollection<Listing>? Listings { get; set; }
    }
}
