using Arzly.Api.Domain.Entities.Listings;

namespace Arzly.Api.Domain.Contracts.Categories
{
    public interface ISubCategoryRepository : IBaseRepository<SubCategory, Guid>
    {
        Task<List<SubCategory>> GetByCategoryIdAsync(Guid categoryId);
        Task<SubCategory?> GetByTitleAsync(string title);
        Task<bool> NameExistsAsync(Guid categoryId, string name, Guid? excludingId = null);
        Task<bool> HasListingsAsync(Guid id);
    }
}
