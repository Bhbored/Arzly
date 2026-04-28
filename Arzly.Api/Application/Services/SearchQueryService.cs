using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.SearchQuery;
using Arzly.Shared.DTOs.Response.SearchQuery;

namespace Arzly.Api.Application.Services
{
    public class SearchQueryService : BaseService<SearchQuery, SearchQueryResponse, SearchQueryAddRequest, SearchQueryUpdateRequest, Guid>, ISearchQueryService
    {
        public SearchQueryService(ISearchQueryRepository repository) : base(repository)
        {
        }

        protected override SearchQueryResponse MapToDto(SearchQuery entity) => entity.ToResponse();
        protected override SearchQuery MapToEntity(SearchQueryAddRequest createDto) => createDto.ToEntity();
        protected override SearchQuery MapToEntity(SearchQueryUpdateRequest updateDto) => updateDto.ToEntity();
    }
}
