using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Infrastructure.Data.DataBaseContext;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
