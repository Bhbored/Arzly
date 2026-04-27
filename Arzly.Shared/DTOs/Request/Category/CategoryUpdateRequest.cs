using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.Category
{
    public class CategoryUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Url(ErrorMessage = "Image URL must be a valid URL.")]
        [MaxLength(2048, ErrorMessage = "Image URL cannot exceed 2048 characters.")]
        public string? ImageUrl { get; set; }
    }
}
