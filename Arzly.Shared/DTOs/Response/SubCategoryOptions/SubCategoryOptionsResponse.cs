namespace Arzly.Shared.DTOs.Response.SubCategoryOptions
{
    public class SubCategoryOptionsResponse
    {
        public Guid Id { get; set; }
        public Guid SubCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ItemsCount { get; set; }
    }
}
