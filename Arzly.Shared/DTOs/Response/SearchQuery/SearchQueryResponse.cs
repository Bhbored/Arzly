namespace Arzly.Shared.DTOs.Response.SearchQuery
{
    public class SearchQueryResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Query { get; set; } = string.Empty;
        public DateTime SearchedAt { get; set; }
    }
}
