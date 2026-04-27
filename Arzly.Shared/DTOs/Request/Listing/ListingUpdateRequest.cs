using Arzly.Shared.Enums.JobListing;
using Arzly.Shared.Enums.Listing;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.Listing
{
    public class ListingUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters.")]
        public string Description { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public double Price { get; set; }

        [Url(ErrorMessage = "Primary image URL must be a valid URL.")]
        [MaxLength(2048, ErrorMessage = "Primary image URL cannot exceed 2048 characters.")]
        public string? PrimaryImageUrl { get; set; }

        public List<string>? ImagesUrl { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Subcategory ID is required.")]
        public Guid SubcategoryId { get; set; }

        [Required(ErrorMessage = "Pickup location ID is required.")]
        public Guid PickupLocationId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        public string PhoneNumber { get; set; } = string.Empty;

        public bool IsPriceNegotiable { get; set; }
        [Required(ErrorMessage = "Contact method is required.")]
        public ContactMethod ContactMethod { get; set; }

       
    }
}
