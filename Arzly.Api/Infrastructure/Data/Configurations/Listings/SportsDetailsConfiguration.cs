using Arzly.Api.Domain.ListingOwned;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Listings
{
    public class SportsDetailsConfiguration : IEntityTypeConfiguration<SportsDetails>
    {
        public void Configure(EntityTypeBuilder<SportsDetails> entity)
        {
            entity.HasOne(e => e.Listing)
                .WithOne(l => l.SportsDetails)
                .HasForeignKey<SportsDetails>(e => e.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasQueryFilter(v => !v.Listing!.IsDeleted);
        }
    }
}
