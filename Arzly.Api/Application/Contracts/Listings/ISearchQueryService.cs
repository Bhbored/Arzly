using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Domain.Entities.Users;
using Arzly.Shared.DTOs.Request.SearchQuery;
using Arzly.Shared.DTOs.Response.SearchQuery;

namespace Arzly.Api.Application.Contracts.Listings
{
    public interface ISearchQueryService
    {
        Task<List<SearchQueryResponse>> GetAllAsync();
        Task<SearchQueryResponse?> GetByIdAsync(Guid id);
        Task<SearchQueryResponse?> CreateAsync(SearchQueryAddRequest? request, Guid userId);
        Task<SearchQueryResponse?> UpdateAsync(SearchQueryUpdateRequest? request, Guid userId);
        Task<bool> DeleteAsync(Guid id);
    }
}
