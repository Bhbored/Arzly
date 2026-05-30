using Arzly.Api.Domain.Contracts.Listings;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Contracts.Listings;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Infrastructure.Data.DataBaseContext;

namespace Arzly.Api.Infrastructure.Repositories.Listings
{
    public class SearchQueryRepository : BaseRepository<SearchQuery, Guid>, ISearchQueryRepository
    {
        public SearchQueryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
