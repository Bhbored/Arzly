using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.SearchQuery;
using Arzly.Shared.DTOs.Response.SearchQuery;

namespace Arzly.Api.Mappings
{
    public static class SearchQueryMapping
    {
        public static SearchQueryResponse ToResponse(this SearchQuery entity)
        {
            return new SearchQueryResponse
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Query = entity.Query,
                SearchedAt = entity.SearchedAt
            };
        }

        public static SearchQuery ToEntity(this SearchQueryAddRequest request)
        {
            return new SearchQuery
            {
                UserId = request.UserId,
                Query = request.Query
            };
        }

        public static SearchQuery ToEntity(this SearchQueryUpdateRequest request)
        {
            return new SearchQuery
            {
                Id = request.Id,
                Query = request.Query
            };
        }

        public static SearchQueryUpdateRequest ToUpdateRequest(this SearchQueryResponse response)
        {
            return new SearchQueryUpdateRequest
            {
                Id = response.Id,
                Query = response.Query
            };
        }
    }
}
