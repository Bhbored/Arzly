using Arzly.Api.Domain.ListingOwned;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Listings
{
    public class PhonesDetailsConfiguration : IEntityTypeConfiguration<PhonesDetails>
    {
        public void Configure(EntityTypeBuilder<PhonesDetails> entity)
        {
            entity.HasOne(e => e.Listing)
                .WithOne(l => l.PhonesDetails)
                .HasForeignKey<PhonesDetails>(e => e.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasQueryFilter(v => !v.Listing!.IsDeleted);
        }
    }
}
