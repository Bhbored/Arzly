using Arzly.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Support
{
    public class TicketMessageConfiguration : IEntityTypeConfiguration<TicketMessage>
    {
        public void Configure(EntityTypeBuilder<TicketMessage> entity)
        {
            entity.HasOne(tm => tm.Ticket)
                .WithMany(t => t.Messages)
                .HasForeignKey(tm => tm.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(tm => tm.Sender)
                .WithMany()
                .HasForeignKey(tm => tm.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(tm => tm.Receiver)
                .WithMany()
                .HasForeignKey(tm => tm.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(tm => tm.TicketId);
            entity.HasIndex(tm => tm.SentAt);

            entity.HasQueryFilter(tm => tm.Ticket.User != null && !tm.Ticket.User.IsDeleted);
        }
    }
}
