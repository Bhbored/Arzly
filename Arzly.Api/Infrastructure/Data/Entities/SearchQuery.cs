using Arzly.Api.Infrastructure.Identity;

namespace Arzly.Api.Infrastructure.Data.Entities
{
    public class SearchQuery
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = "";
        public AppUser User { get; set; } = null!;
        public string Query { get; set; } = "";
        public DateTime SearchedAt { get; set; } = DateTime.UtcNow;
    }
}
