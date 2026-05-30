using Arzly.Api.Domain.ListingOwned;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Listings
{
    public class HobbiesDetailsConfiguration : IEntityTypeConfiguration<HobbiesDetails>
    {
        public void Configure(EntityTypeBuilder<HobbiesDetails> entity)
        {
            entity.HasOne(e => e.Listing)
                .WithOne(l => l.HobbiesDetails)
                .HasForeignKey<HobbiesDetails>(e => e.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasQueryFilter(v => !v.Listing!.IsDeleted);
        }
    }
}
