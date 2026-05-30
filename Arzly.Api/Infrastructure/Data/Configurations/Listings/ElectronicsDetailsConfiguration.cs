using Arzly.Api.Domain.ListingOwned;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Listings
{
    public class ElectronicsDetailsConfiguration : IEntityTypeConfiguration<ElectronicsDetails>
    {
        public void Configure(EntityTypeBuilder<ElectronicsDetails> entity)
        {
            entity.HasOne(e => e.Listing)
                .WithOne(l => l.ElectronicsDetails)
                .HasForeignKey<ElectronicsDetails>(e => e.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasQueryFilter(v => !v.Listing!.IsDeleted);
        }
    }
}
