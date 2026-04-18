using System.Reflection;

namespace Arzly.Api.Infrastructure.Data.Entities
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public string? ImageUrl { get; set; } // Displayed on mobile
        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual ICollection<Listing>? Listings { get; set; }
    }
}
