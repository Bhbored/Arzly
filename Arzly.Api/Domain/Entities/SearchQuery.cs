using Arzly.Api.Infrastructure.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities
{
    public class SearchQuery
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public virtual AppUser User { get; set; } = null!;

        [Required(ErrorMessage = "Query is required.")]
        [MaxLength(200, ErrorMessage = "Query cannot exceed 200 characters.")]
        public string Query { get; set; } = string.Empty;
        public DateTime SearchedAt { get; set; } = DateTime.UtcNow;

    }
}
