using Arzly.Api.Domain.Entities.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Support
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> entity)
        {
            entity.HasOne(t => t.User)
                .WithMany(u => u.CreatedTickets)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.AssignedTo)
                .WithMany()
                .HasForeignKey(t => t.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.RelatedListing)
                .WithMany()
                .HasForeignKey(t => t.ListingId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(t => t.ListingId);

            entity.HasQueryFilter(v => v.User != null && !v.User.IsDeleted);
        }
    }
}
