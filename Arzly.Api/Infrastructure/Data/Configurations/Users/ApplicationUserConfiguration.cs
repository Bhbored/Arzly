using Arzly.Api.Infrastructure.Data.SeedData;
using Arzly.Api.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Users
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> entity)
        {
            entity.HasQueryFilter(u => !u.IsDeleted);

            entity.HasIndex(u => u.FirebaseUid).IsUnique();
            entity.HasIndex(u => u.Email);
            entity.HasIndex(u => u.PublicName);


        }
    }
}
