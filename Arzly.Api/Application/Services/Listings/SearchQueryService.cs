using Arzly.Api.Application.Contracts.Listings;
using Arzly.Api.Domain.Contracts.Listings;
using Arzly.Api.Mappings;
using Arzly.Shared.Constants;
using Arzly.Shared.DTOs.Request.SearchQuery;
using Arzly.Shared.DTOs.Response.SearchQuery;

namespace Arzly.Api.Application.Services.Listings
{
    public class SearchQueryService : ISearchQueryService
    {
        private readonly ISearchQueryRepository _repository;

        public SearchQueryService(ISearchQueryRepository repository) => _repository = repository;

        public async Task<List<SearchQueryResponse>> GetAllAsync() =>
            (await _repository.GetAllAsync()).Select(entity => entity.ToResponse()).ToList();

        public async Task<SearchQueryResponse?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            var entity = await _repository.GetByIdAsync(id)
                ?? throw new ArgumentException($"No Object with this ID {id} Found");
            return entity.ToResponse();
        }

        public async Task<SearchQueryResponse?> CreateAsync(SearchQueryAddRequest? request, Guid userId)
        {
            if (request is null) throw new ArgumentNullException(nameof(request), ExceptionMessages.EmptyAddRequest);
            var entity = request.ToEntity();
            await _repository.AddAsync(entity);
            return entity.ToResponse();
        }

        public async Task<SearchQueryResponse?> UpdateAsync(SearchQueryUpdateRequest? request, Guid userId)
        {
            if (request is null) throw new ArgumentNullException(nameof(request), ExceptionMessages.EmptyUpdateRequest);
            return (await _repository.Update(request.ToEntity())).ToResponse();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            var entity = await _repository.GetByIdAsync(id);
            return entity is not null && await _repository.Delete(entity);
        }
    }
}
