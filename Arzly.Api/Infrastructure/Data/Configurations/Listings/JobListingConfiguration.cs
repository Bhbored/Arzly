using Arzly.Api.Domain.Entities.Listings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Listings
{
    public class JobListingConfiguration : IEntityTypeConfiguration<JobListing>
    {
        public void Configure(EntityTypeBuilder<JobListing> entity)
        {
            entity.HasIndex(j => j.OwnerId);
            entity.HasIndex(j => j.Status);
            entity.HasIndex(j => j.CreatedAt);
            entity.HasIndex(j => j.ExpiresAt);
            entity.HasIndex(j => j.BaseLocation);
            entity.HasIndex(j => j.LocationTitle);

            entity.HasOne(j => j.Owner)
                 .WithMany(j => j.JobListings)
                 .HasForeignKey(j => j.OwnerId)
                 .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(j => j.RelatedChats)
                 .WithOne(c => c.JobListing)
                 .HasForeignKey(c => c.JobListingId)
                 .OnDelete(DeleteBehavior.Cascade);

            entity.HasQueryFilter(j => !j.IsDeleted);
        }
    }
}
