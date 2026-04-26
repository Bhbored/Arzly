using Arzly.Shared.ListingOwned;
using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums.JobListing;
using Arzly.Shared.Enums.Listing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities
{
    public class Listing
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public double Price { get; set; }

        public ListingStatus Status { get; set; } = ListingStatus.Pending;

        [Url]
        [MaxLength(2048)]
        public string? PrimaryImageUrl { get; set; }

        public List<string>? ImagesUrl { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }



        //new Properties 😊

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

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

        public bool IsPriceNegotiable { get; set; } = false;

        public bool IsPromoted { get; set; } = false;
        public PromotionType? PromotionType { get; set; }
        public DateTime? PromotionStartDate { get; set; }
        public DateTime? PromotionEndDate { get; set; }
        public ContactMethod MethodOfContact { get; set; } = ContactMethod.InAppChat;

        // Foreign keys
        [Required]
        public string OwnerId { get; set; } = string.Empty;

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public Guid SubcategoryId { get; set; }

        [Required]
        public Guid PickupLocationId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(OwnerId))]
        public virtual AppUser Owner { get; set; } = null!;

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = null!;

        [ForeignKey(nameof(PickupLocationId))]
        public virtual PickupLocation PickupLocation { get; set; } = null!;

        [ForeignKey(nameof(SubcategoryId))]
        public virtual SubCategory SubCategory { get; set; } = null!; 

        public virtual ICollection<Chat>? RelatedChats { get; set; }
        public virtual ICollection<SavedListing>? SavedByUsers { get; set; }
    }
}
