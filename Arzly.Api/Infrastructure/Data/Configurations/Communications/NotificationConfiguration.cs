using Arzly.Api.Domain.Entities.Communications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Communications;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> entity)
    {
        entity.HasIndex(x => new { x.UserId, x.CreatedAt });
        entity.HasIndex(x => new { x.UserId, x.IsRead });
        entity.HasIndex(x => x.ExpiresAt);
        entity.Property(x => x.Metadata).HasMaxLength(4000);

        entity.HasOne(x => x.User).WithMany(x => x.Notifications)
            .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        entity.HasOne(x => x.Chatter).WithMany()
            .HasForeignKey(x => x.ChatterId).OnDelete(DeleteBehavior.NoAction);
        entity.HasOne(x => x.Listing).WithMany()
            .HasForeignKey(x => x.ListingId).OnDelete(DeleteBehavior.SetNull);
    }
}
