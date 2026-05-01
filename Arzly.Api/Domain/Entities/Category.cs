using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Arzly.Api.Domain.Entities
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        public int ItemsCount { get; set; }

        [Url(ErrorMessage = "Image URL must be a valid URL.")]
        [MaxLength(2048, ErrorMessage = "Image URL cannot exceed 2048 characters.")]
        public string? ImageUrl { get; set; }

        public virtual ICollection<SubCategory>? SubCategories { get; set; }
        public virtual ICollection<Listing>? Listings { get; set; }
    }
}
