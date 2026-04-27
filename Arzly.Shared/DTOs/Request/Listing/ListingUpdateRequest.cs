using Arzly.Shared.Enums.JobListing;
using Arzly.Shared.Enums.Listing;
using Arzly.Shared.ListingOwned;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.Listing
{
    public class ListingUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public double Price { get; set; }

        [Url]
        [MaxLength(2048)]
        public string? PrimaryImageUrl { get; set; }

        public List<string>? ImagesUrl { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public Guid SubcategoryId { get; set; }

        [Required]
        public Guid PickupLocationId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        public bool IsPriceNegotiable { get; set; }
        [Required]
        public ContactMethod ContactMethod { get; set; }

        // Detail objects
        public VehiclesDetails? VehiclesDetails { get; set; }
        public RealEstateDetails? RealEstateDetails { get; set; }
        public PhonesDetails? PhonesDetails { get; set; }
        public ElectronicsDetails? ElectronicsDetails { get; set; }
        public FurnitureDetails? FurnitureDetails { get; set; }
        public PetsDetails? PetsDetails { get; set; }
        public BabyChildDetails? BabyChildDetails { get; set; }
        public SportsDetails? SportsDetails { get; set; }
        public HobbiesDetails? HobbiesDetails { get; set; }
        public FashionDetails? FashionDetails { get; set; }
        public ServicesDetails? ServicesDetails { get; set; }
    }
}
