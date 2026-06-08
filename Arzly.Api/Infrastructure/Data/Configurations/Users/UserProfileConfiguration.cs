using Arzly.Api.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Users
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {

        public void Configure(EntityTypeBuilder<UserProfile> entity)
        {
            entity.HasIndex(u => u.UserId).IsUnique();
            entity.HasIndex(u => u.IsStore);
            entity.HasIndex(u => u.IsVerified);
        }
    }
}
