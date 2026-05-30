using Arzly.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arzly.Api.Infrastructure.Data.Configurations.Users
{
    public class SearchQueryConfiguration : IEntityTypeConfiguration<SearchQuery>
    {
        public void Configure(EntityTypeBuilder<SearchQuery> entity)
        {
            entity.HasOne(sq => sq.User)
                .WithMany(u => u.SearchHistory)
                .HasForeignKey(sq => sq.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(sq => sq.UserId);
            entity.HasIndex(sq => sq.SearchedAt);
            entity.HasQueryFilter(v => v.User != null && !v.User.IsDeleted);
        }
    }
}
