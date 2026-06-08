using Arzly.Api.Domain.Entities.Listings;

namespace Arzly.Api.Domain.Contracts.Categories
{
    public interface ICategoryRepository : IBaseRepository<Category, Guid>
    {
    }
}
