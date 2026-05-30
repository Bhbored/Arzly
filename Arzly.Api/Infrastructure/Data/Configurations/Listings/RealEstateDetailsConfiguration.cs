using Arzly.Api.Domain.ListingOwned;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Listings
{
    public class RealEstateDetailsConfiguration : IEntityTypeConfiguration<RealEstateDetails>
    {
        public void Configure(EntityTypeBuilder<RealEstateDetails> entity)
        {
            entity.HasOne(e => e.Listing)
                .WithOne(l => l.RealEstateDetails)
                .HasForeignKey<RealEstateDetails>(e => e.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasQueryFilter(v => !v.Listing!.IsDeleted);
        }
    }
}
