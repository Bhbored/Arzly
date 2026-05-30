using Arzly.Api.Domain.ListingOwned;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Listings
{
    public class ServicesDetailsConfiguration : IEntityTypeConfiguration<ServicesDetails>
    {
        public void Configure(EntityTypeBuilder<ServicesDetails> entity)
        {
            entity.HasOne(e => e.Listing)
                .WithOne(l => l.ServicesDetails)
                .HasForeignKey<ServicesDetails>(e => e.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasQueryFilter(v => !v.Listing!.IsDeleted);
        }
    }
}
