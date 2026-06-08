using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Domain.Entities.Users;
using Arzly.Shared.DTOs.Request.SearchQuery;
using Arzly.Shared.DTOs.Response.SearchQuery;

namespace Arzly.Api.Application.Contracts.Listings
{
    public interface ISearchQueryService : IBaseService<SearchQuery, SearchQueryResponse, SearchQueryAddRequest, SearchQueryUpdateRequest, Guid>
    {
    }
}
