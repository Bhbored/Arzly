using Arzly.Api.Domain.Entities.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Support
{
    public class TicketAttachmentConfiguration : IEntityTypeConfiguration<TicketAttachment>
    {
        public void Configure(EntityTypeBuilder<TicketAttachment> entity)
        {
            entity.HasOne(ta => ta.Ticket)
                .WithMany(t => t.Attachments)
                .HasForeignKey(ta => ta.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ta => ta.Uploader)
                .WithMany()
                .HasForeignKey(ta => ta.UploaderId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(ta => ta.TicketId);
            entity.HasIndex(ta => ta.UploadedAt);

            entity.HasQueryFilter(ta => ta.Ticket.User != null && !ta.Ticket.User.IsDeleted);
        }
    }
}
