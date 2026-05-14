using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category, Guid>, ICategoryRepository
    {
        private readonly AppDbContext _db;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _db = context;
        }



        public override async Task<List<Category>> GetAllAsync()
        {
            return await _db.Categories.AsNoTracking()
                                 .OrderBy(x => x.Priority)
                                 .ToListAsync();
        }

    }
}
