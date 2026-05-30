using Arzly.Api.Domain.ListingOwned;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Listings
{
    public class BabyChildDetailsConfiguration : IEntityTypeConfiguration<BabyChildDetails>
    {
        public void Configure(EntityTypeBuilder<BabyChildDetails> entity)
        {
            entity.HasOne(e => e.Listing)
                .WithOne(l => l.BabyChildDetails)
                .HasForeignKey<BabyChildDetails>(e => e.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasQueryFilter(v => !v.Listing!.IsDeleted);
        }
    }
}
