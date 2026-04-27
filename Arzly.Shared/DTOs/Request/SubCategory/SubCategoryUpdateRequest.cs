using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.SubCategory
{
    public class SubCategoryUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
