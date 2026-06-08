using Arzly.Api.Domain.Entities.Listings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Listings
{
    public class SavedListingConfiguration : IEntityTypeConfiguration<SavedListing>
    {
        public void Configure(EntityTypeBuilder<SavedListing> entity)
        {
            entity.HasOne(sl => sl.User)
                 .WithMany(u => u.SavedListings)
                 .HasForeignKey(sl => sl.UserId)
                 .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(sl => sl.Listing)
                  .WithMany(l => l.SavedByUsers)
                  .HasForeignKey(sl => sl.ListingId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(s => new { s.UserId, s.ListingId }).IsUnique();
            entity.HasQueryFilter(v => v.Listing != null && !v.Listing.IsDeleted);
        }
    }
}
