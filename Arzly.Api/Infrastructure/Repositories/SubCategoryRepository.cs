using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class SubCategoryRepository : BaseRepository<SubCategory, Guid>, ISubCategoryRepository
    {
        private readonly AppDbContext _db;
        private readonly ILogger<SubCategoryRepository> _logger;

        public SubCategoryRepository(AppDbContext context, ILogger<SubCategoryRepository> logger) : base(context)
        {
            _db = context;
            _logger = logger;
        }

        public async Task<List<SubCategory>> GetByCategoryIdAsync(Guid categoryId)
        {
            _logger.LogInformation($"{GetType().Name} - GetByCategoryIdAsync has been reached");

            return await _db.SubCategories
                .AsNoTracking()
                .Where(x => x.CategoryId == categoryId)
                .OrderBy(x=>x.Priority)
                .ToListAsync();
        }
    }
}
