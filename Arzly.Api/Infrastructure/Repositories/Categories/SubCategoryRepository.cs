using Arzly.Api.Domain.Contracts.Categories;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories.Categories
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

        public async Task<SubCategory?> GetByTitleAsync(string title)
        {
            _logger.LogInformation($"{GetType().Name} - GetByTitleAsync has been reached");

            return await _db.SubCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == title);
        }

        public Task<bool> NameExistsAsync(Guid categoryId, string name, Guid? excludingId = null) =>
            _db.SubCategories.AnyAsync(x => x.CategoryId == categoryId &&
                x.Name.ToLower() == name.ToLower() &&
                (excludingId == null || x.Id != excludingId));

        public Task<bool> HasListingsAsync(Guid id) =>
            _db.Listings.AnyAsync(x => x.SubcategoryId == id);

        public override async Task<SubCategory> Update(SubCategory entity)
        {
            var stored = await _db.SubCategories.FirstOrDefaultAsync(x => x.Id == entity.Id)
                ?? throw new ArgumentException("Subcategory not found");
            stored.CategoryId = entity.CategoryId;
            stored.Name = entity.Name;
            stored.Description = entity.Description;
            await _db.SaveChangesAsync();
            return stored;
        }
    }
}
