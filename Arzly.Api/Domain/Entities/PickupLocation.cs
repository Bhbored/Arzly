using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums.Listing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities
{
    public class PickupLocation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public virtual AppUser User { get; set; } = null!;

        public LocationLabel Label { get; set; } = LocationLabel.Home;

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(500, ErrorMessage = "Address cannot exceed 500 characters.")]
        public string Address { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "Notes cannot exceed 200 characters.")]
        public string? Notes { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

        public bool IsDefault { get; set; }

        public virtual ICollection<Listing>? Listings { get; set; }
    }
}
