using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.SearchQuery
{
    public class SearchQueryUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Query is required.")]
        [MaxLength(200, ErrorMessage = "Query cannot exceed 200 characters.")]
        public string Query { get; set; } = string.Empty;
    }
}
