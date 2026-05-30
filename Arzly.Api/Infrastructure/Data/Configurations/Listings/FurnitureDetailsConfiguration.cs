using Arzly.Api.Domain.ListingOwned;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Listings
{
    public class FurnitureDetailsConfiguration : IEntityTypeConfiguration<FurnitureDetails>
    {
        public void Configure(EntityTypeBuilder<FurnitureDetails> entity)
        {
            entity.HasOne(e => e.Listing)
                .WithOne(l => l.FurnitureDetails)
                .HasForeignKey<FurnitureDetails>(e => e.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasQueryFilter(v => !v.Listing!.IsDeleted);
        }
    }
}
