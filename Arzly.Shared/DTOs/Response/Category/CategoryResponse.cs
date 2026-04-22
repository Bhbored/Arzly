using Arzly.Shared.DTOs.Response.Listing;
using Arzly.Shared.DTOs.Response.SubCategory;

namespace Arzly.Shared.DTOs.Response.Category
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ItemsCount { get; set; }
        public string? ImageUrl { get; set; }
    }
}
