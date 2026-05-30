using Arzly.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Listings
{
    public class PickupLocationConfiguration : IEntityTypeConfiguration<PickupLocation>
    {
        public void Configure(EntityTypeBuilder<PickupLocation> entity)
        {
            entity.HasQueryFilter(p => !p.IsDeleted);

            entity.HasOne(p => p.User)
                 .WithMany(u => u.DeliveryLocations)
                 .HasForeignKey(p => p.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
