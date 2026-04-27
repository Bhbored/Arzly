using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Infrastructure.Data.DataBaseContext;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class SubCategoryRepository : BaseRepository<SubCategory, Guid>, ISubCategoryRepository
    {
        public SubCategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
