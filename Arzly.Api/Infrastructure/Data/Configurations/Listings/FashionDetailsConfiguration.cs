using Arzly.Api.Domain.ListingOwned;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Listings
{
    public class FashionDetailsConfiguration : IEntityTypeConfiguration<FashionDetails>
    {
        public void Configure(EntityTypeBuilder<FashionDetails> entity)
        {
            entity.HasOne(e => e.Listing)
                .WithOne(l => l.FashionDetails)
                .HasForeignKey<FashionDetails>(e => e.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasQueryFilter(v => !v.Listing!.IsDeleted);
        }
    }
}
