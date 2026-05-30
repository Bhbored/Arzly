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
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required(ErrorMessage = "Query is required.")]
        [MaxLength(200, ErrorMessage = "Query cannot exceed 200 characters.")]
        public string Query { get; set; } = string.Empty;
        public DateTime SearchedAt { get; set; } = DateTime.UtcNow;

    }
}
