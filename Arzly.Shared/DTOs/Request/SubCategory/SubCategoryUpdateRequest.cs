namespace Arzly.Shared.DTOs.Request.SubCategory
{
    public class SubCategoryUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
