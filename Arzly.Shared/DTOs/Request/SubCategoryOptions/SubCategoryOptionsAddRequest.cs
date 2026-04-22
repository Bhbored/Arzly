namespace Arzly.Shared.DTOs.Request.SubCategoryOptions
{
    public class SubCategoryOptionsAddRequest
    {
        public Guid SubCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
