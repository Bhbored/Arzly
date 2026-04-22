namespace Arzly.Shared.DTOs.Request.SubCategory
{
    public class SubCategoryAddRequest
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
