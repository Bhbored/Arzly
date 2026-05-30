using Arzly.Api.Domain.ListingOwned;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Listings
{
    public class PetsDetailsConfiguration : IEntityTypeConfiguration<PetsDetails>
    {
        public void Configure(EntityTypeBuilder<PetsDetails> entity)
        {
            entity.HasOne(e => e.Listing)
                .WithOne(l => l.PetsDetails)
                .HasForeignKey<PetsDetails>(e => e.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasQueryFilter(v => !v.Listing!.IsDeleted);
        }
    }
}
