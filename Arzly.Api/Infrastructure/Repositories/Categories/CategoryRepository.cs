using Arzly.Api.Domain.Contracts.Categories;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories.Categories
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

        public Task<bool> NameExistsAsync(string name, Guid? excludingId = null) =>
            _db.Categories.AnyAsync(x => x.Name.ToLower() == name.ToLower() &&
                (excludingId == null || x.Id != excludingId));

        public async Task<bool> HasDependentsAsync(Guid id)
        {
            if (await _db.SubCategories.AnyAsync(x => x.CategoryId == id))
                return true;
            return await _db.Listings.AnyAsync(x => x.CategoryId == id);
        }

        public override async Task<Category> Update(Category entity)
        {
            var stored = await _db.Categories.FirstOrDefaultAsync(x => x.Id == entity.Id)
                ?? throw new ArgumentException("Category not found");
            stored.Name = entity.Name;
            stored.Description = entity.Description;
            stored.ImageUrl = entity.ImageUrl;
            await _db.SaveChangesAsync();
            return stored;
        }

    }
}
