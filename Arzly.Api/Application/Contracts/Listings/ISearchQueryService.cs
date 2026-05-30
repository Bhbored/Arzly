using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.SearchQuery;
using Arzly.Shared.DTOs.Response.SearchQuery;

namespace Arzly.Api.Application.Contracts.Listings
{
    public interface ISearchQueryService : IBaseService<SearchQuery, SearchQueryResponse, SearchQueryAddRequest, SearchQueryUpdateRequest, Guid>
    {
    }
}
