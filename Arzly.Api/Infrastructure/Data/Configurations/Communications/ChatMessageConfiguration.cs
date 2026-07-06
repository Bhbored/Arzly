using Arzly.Api.Domain.Entities.Communications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Communications
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> entity)
        {
            entity.HasIndex(m => m.ChatId);
            entity.HasIndex(m => m.SentAt);

            entity.HasOne(cm => cm.Chat)
                 .WithMany(c => c.Messages)
                 .HasForeignKey(cm => cm.ChatId)
                 .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(m => m.Sender)
                 .WithMany()
                 .HasForeignKey(m => m.SenderId)
                 .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(m => m.Receiver)
                 .WithMany()
                 .HasForeignKey(m => m.ReceiverId)
                 .OnDelete(DeleteBehavior.Restrict);

            entity.Property(m => m.IsRead).HasDefaultValue(false);

            entity.HasQueryFilter(cm => !cm.IsDeleted &&
                !cm.Chat.IsDeleted &&
                (cm.Chat.Listing == null || !cm.Chat.Listing.IsDeleted) &&
                (cm.Chat.JobListing == null || !cm.Chat.JobListing.IsDeleted));
        }
    }
}
