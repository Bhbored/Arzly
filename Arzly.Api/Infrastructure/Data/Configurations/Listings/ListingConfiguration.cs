using Arzly.Api.Domain.Entities.Listings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Listings
{
    public class ListingConfiguration : IEntityTypeConfiguration<Listing>
    {
        public void Configure(EntityTypeBuilder<Listing> entity)
        {
            entity.HasIndex(l => l.CategoryId);
            entity.HasIndex(l => l.SubcategoryId);
            entity.HasIndex(l => l.OwnerId);
            entity.HasIndex(l => l.PickupLocationId);
            entity.HasIndex(l => l.Status);
            entity.HasIndex(l => l.CreatedAt);

            entity.HasOne(l => l.Category)
                 .WithMany(c => c.Listings)
                 .HasForeignKey(l => l.CategoryId)
                 .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(l => l.SubCategory)
                 .WithMany(s => s.Listings)
                 .HasForeignKey(l => l.SubcategoryId)
                 .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(l => l.Owner)
                 .WithMany(u => u.Listings)
                 .HasForeignKey(l => l.OwnerId)
                 .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(l => l.PickupLocation)
                 .WithMany(p => p.Listings)
                 .HasForeignKey(l => l.PickupLocationId)
                 .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(l => l.RelatedChats)
                 .WithOne(c => c.Listing)
                 .HasForeignKey(c => c.ListingId)
                 .OnDelete(DeleteBehavior.Cascade);

            entity.HasQueryFilter(l => !l.IsDeleted);
        }
    }
}
