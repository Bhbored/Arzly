using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Domain.Contracts.Categories
{
    public interface ICategoryRepository : IBaseRepository<Category, Guid>
    {
    }
}
