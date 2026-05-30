using Arzly.Api.Domain.ListingOwned;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Listings
{
    public class VehiclesDetailsConfiguration : IEntityTypeConfiguration<VehiclesDetails>
    {
        public void Configure(EntityTypeBuilder<VehiclesDetails> entity)
        {
            entity.HasOne(e => e.Listing)
                .WithOne(l => l.VehiclesDetails)
                .HasForeignKey<VehiclesDetails>(e => e.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasQueryFilter(v => !v.Listing!.IsDeleted);
        }
    }
}
