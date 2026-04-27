using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.SubCategory
{
    public class SubCategoryUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }
    }
}
