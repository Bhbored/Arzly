using Arzly.Api.Domain.ListingOwned;
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

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string Description { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public double Price { get; set; }

        public ListingStatus Status { get; set; } = ListingStatus.Pending;

        [Url(ErrorMessage = "Primary image URL must be a valid URL.")]
        [MaxLength(2048, ErrorMessage = "Primary image URL cannot exceed 2048 characters.")]
        public string? PrimaryImageUrl { get; set; }

        public List<string>? ImagesUrl { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }



        //new Properties 


        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        public string PhoneNumber { get; set; } = string.Empty;


        public bool IsPriceNegotiable { get; set; } = false;

        public bool IsPromoted { get; set; } = false;
        public PromotionType? PromotionType { get; set; }
        public DateTime? PromotionStartDate { get; set; }
        public DateTime? PromotionEndDate { get; set; }
        [Required(ErrorMessage = "Contact method is required.")]
        public ContactMethod ContactMethod { get; set; }

        // Foreign keys
        [Required(ErrorMessage = "Owner ID is required.")]
        public string OwnerId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category ID is required.")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Subcategory ID is required.")]
        public Guid SubcategoryId { get; set; }

        [Required(ErrorMessage = "Pickup location ID is required.")]
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

       


        //pre-owned

        public VehiclesDetails? VehiclesDetails { get; set; }
        public virtual RealEstateDetails? RealEstateDetails { get; set; }
        public virtual PhonesDetails? PhonesDetails { get; set; }
        public virtual ElectronicsDetails? ElectronicsDetails { get; set; }
        public virtual FurnitureDetails? FurnitureDetails { get; set; }
        public virtual PetsDetails? PetsDetails { get; set; }
        public virtual BabyChildDetails? BabyChildDetails { get; set; }
        public virtual SportsDetails? SportsDetails { get; set; }
        public virtual HobbiesDetails? HobbiesDetails { get; set; }
        public virtual FashionDetails? FashionDetails { get; set; }
        public virtual ServicesDetails? ServicesDetails { get; set; }

        public override string ToString()
        {
            return $"Id={Id}, Title={Title}, Description={Description}, Price={Price}, Status={Status}, " +
                   $"PrimaryImageUrl={PrimaryImageUrl}, ImagesCount={ImagesUrl?.Count ?? 0}, CreatedAt={CreatedAt}, " +
                   $"UpdatedAt={UpdatedAt}, Name={Name}, PhoneNumber={PhoneNumber}, IsPriceNegotiable={IsPriceNegotiable}, " +
                   $"IsPromoted={IsPromoted}, PromotionType={PromotionType}, PromotionStartDate={PromotionStartDate}, " +
                   $"PromotionEndDate={PromotionEndDate}, ContactMethod={ContactMethod}, OwnerId={OwnerId}, " +
                   $"CategoryId={CategoryId}, SubcategoryId={SubcategoryId}, PickupLocationId={PickupLocationId}";
        }
    }
}
