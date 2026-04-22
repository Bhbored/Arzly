namespace Arzly.Shared.DTOs.Request.SearchQuery
{
    public class SearchQueryUpdateRequest
    {
        public Guid Id { get; set; }
        public string Query { get; set; } = string.Empty;
    }
}
