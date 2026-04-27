using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.SearchQuery
{
    public class SearchQueryAddRequest
    {
        [Required(ErrorMessage = "User ID is required.")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Query is required.")]
        [MaxLength(200, ErrorMessage = "Query cannot exceed 200 characters.")]
        public string Query { get; set; } = string.Empty;
    }
}
