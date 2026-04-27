using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.SearchQuery
{
    public class SearchQueryUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Query { get; set; } = string.Empty;
    }
}
