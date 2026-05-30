using Arzly.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Users
{
    public class UserPreferenceConfiguration : IEntityTypeConfiguration<UserPreference>
    {
        public void Configure(EntityTypeBuilder<UserPreference> entity)
        {
            entity.HasOne(up => up.User)
                    .WithOne(u => u.Preferences)
                    .HasForeignKey<UserPreference>(up => up.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            entity.HasQueryFilter(v => v.User != null && !v.User.IsDeleted);
        }
    }
}
