using Arzly.Api.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Users
{
    public class UserActivityLogConfiguration : IEntityTypeConfiguration<UserActivityLog>
    {
        public void Configure(EntityTypeBuilder<UserActivityLog> entity)
        {
            entity.HasIndex(l => l.ActorId);
            entity.HasIndex(l => l.Timestamp);

            entity.HasQueryFilter(v => v.Actor != null && !v.Actor.IsDeleted);
        }
    }
}
