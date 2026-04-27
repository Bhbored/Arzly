using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.SearchQuery
{
    public class SearchQueryAddRequest
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Query { get; set; } = string.Empty;
    }
}
