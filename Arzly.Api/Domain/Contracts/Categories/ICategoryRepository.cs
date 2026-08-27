using Arzly.Api.Domain.Entities.Listings;

namespace Arzly.Api.Domain.Contracts.Categories
{
    public interface ICategoryRepository : IBaseRepository<Category, Guid>
    {
        Task<bool> NameExistsAsync(string name, Guid? excludingId = null);
        Task<bool> HasDependentsAsync(Guid id);
    }
}
