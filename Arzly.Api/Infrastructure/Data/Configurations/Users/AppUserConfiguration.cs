using Arzly.Api.Infrastructure.Data.SeedData;
using Arzly.Api.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Users
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> entity)
        {
            entity.HasQueryFilter(u => !u.IsDeleted);

            entity.HasIndex(u => u.FirebaseUid).IsUnique();
            entity.HasIndex(u => u.Email);
            entity.HasIndex(u => u.PublicName);

            foreach (var item in AppUserSeed.Users)
                entity.HasData(item);
        }
    }
}
