using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities
{
    public class SubCategoryOptions
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid SubCategoryId { get; set; }

        [ForeignKey(nameof(SubCategoryId))]
        public virtual SubCategory? SubCategory { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int ItemsCount { get; set; }

        public virtual ICollection<Listing>? Listings { get; set; }
    }
}
