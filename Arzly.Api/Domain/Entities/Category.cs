using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Arzly.Api.Domain.Entities
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int ItemsCount { get; set; }

        [Url]
        [MaxLength(2048)]
        public string? ImageUrl { get; set; }

        public virtual ICollection<SubCategory>? SubCategories { get; set; }
        public virtual ICollection<Listing>? Listings { get; set; }
    }
}
