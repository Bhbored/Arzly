namespace Arzly.Shared.DTOs.Response.SubCategory
{
    public class SubCategoryResponse
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ItemsCount { get; set; }
    }
}
