using Arzly.Api.Application.Contracts.Listings;
using Arzly.Api.Domain.Contracts.Listings;
using Arzly.Api.Mappings;
using Arzly.Shared.Constants;
using Arzly.Shared.DTOs.Request.SavedListing;
using Arzly.Shared.DTOs.Response.SavedListing;

namespace Arzly.Api.Application.Services.Listings
{
    public class SavedListingService : ISavedListingService
    {
        private readonly ISavedListingRepository _repository;
        private readonly ILogger<SavedListingService> _logger;
        private readonly IListingService _listingService;

        public SavedListingService(ISavedListingRepository repository, ILogger<SavedListingService> logger,IListingService listingService)
        {
            _repository = repository;
            _logger = logger;
            _listingService = listingService;
        }

        public async Task<List<SavedListingResponse>> GetAllAsync(Guid userId)
        {
            _logger.LogInformation("{Service}.GetAllAsync({UserId}) - Before", GetType().Name, userId);

            if (userId == Guid.Empty)
            {
                _logger.LogError("{Service}.GetAllAsync - Empty userId provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            var entities = await _repository.GetByUserIdAsync(userId);
            var result = entities.ConvertAll(x => x.ToResponse());
            foreach(var x in result)
            {
                x.Listing = await _listingService.GetByIdAsync(x.ListingId);
            }

            _logger.LogInformation("{Service}.GetAllAsync({UserId}) - After, count {Count}", GetType().Name, userId, result.Count);
            return result;
        }

        public async Task<SavedListingResponse?> GetByIdAsync(Guid id, Guid userId)
        {
            _logger.LogInformation("{Service}.GetByIdAsync({Id}) - Before", GetType().Name, id);

            if (id == Guid.Empty)
            {
                _logger.LogError("{Service}.GetByIdAsync - Empty id provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            var entity = await _repository.GetByIdAsync(id, userId);
            if (entity is null)
            {
                _logger.LogError("{Service}.GetByIdAsync - No SavedListing found with id {Id}", GetType().Name, id);
                throw new UnauthorizedAccessException("The saved listing does not belong to the current user");
            }

            _logger.LogInformation("{Service}.GetByIdAsync({Id}) - After", GetType().Name, id);
            return entity.ToResponse();
        }

        public async Task<SavedListingResponse?> CreateAsync(SavedListingAddRequest createDto, Guid userId)
        {
            _logger.LogInformation("{Service}.CreateAsync - Before", GetType().Name);

            if (createDto is null)
            {
                _logger.LogError("{Service}.CreateAsync - Empty createDto provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.EmptyAddRequest);
            }

            if (userId == Guid.Empty)
            {
                _logger.LogError("{Service}.CreateAsync - Empty userId provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            await _listingService.GetByIdAsync(createDto.ListingId);

            var oldentity = await _repository.GetByListingIdAsync(createDto.ListingId, userId);
            if (oldentity != null)
            {
                await _repository.UndeleteAsync(oldentity.Id, userId);
                oldentity.DeletedAt = null;
                return oldentity.ToResponse();
            }
            else
            {
                var entity = createDto.ToEntity();
                entity.Id = Guid.NewGuid();
                entity.UserId = userId;
                entity.SavedAt = DateTime.UtcNow;
                await _repository.CreateAsync(entity);

                _logger.LogInformation("{Service}.CreateAsync - " +
                    "After, created SavedListing with id {Id}", GetType().Name, entity.Id);
                return entity.ToResponse();
            }

        }

        public async Task DeleteAsync(Guid id, Guid userId)
        {
            _logger.LogInformation("{Service}.DeleteAsync({Id}) - Before", GetType().Name, id);

            if (id == Guid.Empty)
            {
                _logger.LogError("{Service}.DeleteAsync - Empty id provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            var deleted = await _repository.SoftDeleteAsync(id, userId);
            if (!deleted)
            {
                _logger.LogError("{Service}.DeleteAsync - No SavedListing found with id {Id}", GetType().Name, id);
                throw new UnauthorizedAccessException("The saved listing does not belong to the current user");
            }

            _logger.LogInformation("{Service}.DeleteAsync({Id}) - After", GetType().Name, id);
        }

        public async Task UndeleteAsync(Guid id, Guid userId)
        {
            _logger.LogInformation("{Service}.UndeleteAsync({Id}) - Before", GetType().Name, id);

            if (id == Guid.Empty)
            {
                _logger.LogError("{Service}.UndeleteAsync - Empty id provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            var restored = await _repository.UndeleteAsync(id, userId);
            if (!restored)
            {
                _logger.LogError("{Service}.UndeleteAsync - No SavedListing found with id {Id}", GetType().Name, id);
                throw new UnauthorizedAccessException("The saved listing does not belong to the current user");
            }

            _logger.LogInformation("{Service}.UndeleteAsync({Id}) - After", GetType().Name, id);
        }
    }
}
