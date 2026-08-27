using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Infrastructure.Data.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Listings
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> entity)
        {
            entity.HasIndex(x => new { x.CategoryId, x.Name }).IsUnique();
        }
    }
}
