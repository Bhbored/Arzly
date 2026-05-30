using Arzly.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Users
{
    public class UserReportConfiguration : IEntityTypeConfiguration<UserReport>
    {
        public void Configure(EntityTypeBuilder<UserReport> entity)
        {
            entity.HasOne(r => r.Reporter)
                 .WithMany(u => u.ReportsMade)
                 .HasForeignKey(r => r.ReporterId)
                 .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.ReportedUser)
                 .WithMany(u => u.ReportsReceived)
                 .HasForeignKey(r => r.ReportedUserId)
                 .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(r => r.ReporterId);
            entity.HasIndex(r => r.ReportedUserId);
            entity.HasIndex(r => r.ChatId);
            entity.HasIndex(r => r.IsResolved);
            entity.HasIndex(r => r.CreatedAt);

            entity.HasQueryFilter(v => v.Reporter != null && !v.Reporter.IsDeleted);
            entity.HasQueryFilter(v => v.ReportedUser != null && !v.ReportedUser.IsDeleted);
        }
    }
}
