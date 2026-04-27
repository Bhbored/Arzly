using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.Category
{
    public class CategoryAddRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Url]
        [MaxLength(2048)]
        public string? ImageUrl { get; set; }
    }
}
