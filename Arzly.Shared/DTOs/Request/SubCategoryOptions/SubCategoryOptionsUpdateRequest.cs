namespace Arzly.Shared.DTOs.Request.SubCategoryOptions
{
    public class SubCategoryOptionsUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid SubCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
