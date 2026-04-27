using Arzly.Shared.ListingOwned;
using Arzly.Shared.Enums.Listing;
using Arzly.Shared.Enums.JobListing;

namespace Arzly.Shared.DTOs.Response.Listing
{
    public class ListingResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public bool IsPriceNegotiable { get; set; }
        public ListingStatus Status { get; set; }
        public string? PrimaryImageUrl { get; set; }
        public List<string>? ImagesUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Owner
        public string OwnerId { get; set; } = string.Empty;

        // Category
        public Guid CategoryId { get; set; }

        // SubCategory
        public Guid SubcategoryId { get; set; }

        // Location
        public Guid PickupLocationId { get; set; }

        // Contact
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public ContactMethod ContactMethod { get; set; }

        // Promotion
        public bool IsPromoted { get; set; }
        public PromotionType? PromotionType { get; set; }
        public DateTime? PromotionStartDate { get; set; }
        public DateTime? PromotionEndDate { get; set; }

        // Detail objects (only one non-null)
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



