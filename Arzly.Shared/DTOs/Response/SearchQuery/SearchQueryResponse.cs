namespace Arzly.Shared.DTOs.Response.SearchQuery
{
    public class SearchQueryResponse
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Query { get; set; } = string.Empty;
        public DateTime SearchedAt { get; set; }
    }
}
