using Arzly.Api.Domain.Entities.Communications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Communications
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> entity)
        {
            entity.HasIndex(c => c.InitiatorId);
            entity.HasIndex(c => c.ReceiverId);
            entity.HasIndex(c => c.ListingId);
            entity.HasIndex(c => c.JobListingId);
            entity.HasIndex(c => c.LastActivity);
            entity.HasIndex(c => c.IsArchived);

            entity.HasOne(c => c.Initiator)
                 .WithMany(u => u.ChatsInitiated)
                 .HasForeignKey(c => c.InitiatorId)
                 .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.Receiver)
                 .WithMany(u => u.ChatsReceived)
                 .HasForeignKey(c => c.ReceiverId)
                 .OnDelete(DeleteBehavior.Restrict);

            entity.HasQueryFilter(c => !c.IsDeleted &&
                (c.Listing == null || !c.Listing.IsDeleted) &&
                (c.JobListing == null || !c.JobListing.IsDeleted));
        }
    }
}
