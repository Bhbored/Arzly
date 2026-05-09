using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Domain.Contracts
{
    public interface ISubCategoryRepository : IBaseRepository<SubCategory, Guid>
    {
        Task<List<SubCategory>> GetByCategoryIdAsync(Guid categoryId);
    }
}
