using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Domain.Entities.Users;

namespace Arzly.Api.Domain.Contracts.Listings
{
    public interface ISearchQueryRepository : IBaseRepository<SearchQuery, Guid>
    {
    }
}
