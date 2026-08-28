using Arzly.Api.Application.Contracts.Locations;
using Arzly.Api.Domain.Contracts.Locations;
using Arzly.Api.Mappings;
using Arzly.Shared.Constants;
using Arzly.Shared.DTOs.Request.PickupLocation;
using Arzly.Shared.DTOs.Response.PickupLocation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Arzly.Api.Application.Services.Locations
{
    public class PickupLocationService : IPickupLocationService
    {
        private readonly IPickupLocationRepository _pickupLocationRepo;
        private readonly ILogger<PickupLocationService> _logger;
        public PickupLocationService(IPickupLocationRepository repository,
            ILogger<PickupLocationService> logger)
        {
            _pickupLocationRepo = repository;
            _logger = logger;
        }

        public async Task<List<PickupLocationResponse>> GetByUserId(Guid? userId)
        {
            _logger.LogInformation($"{GetType().Name} - GetByUserId Has been reached");

            var entities = await _pickupLocationRepo.GetByUserId(userId!.Value);

            return entities.Select(x => x.ToResponse()).ToList();
        }

        public async Task<List<PickupLocationResponse>> GetAllAsync() =>
            (await _pickupLocationRepo.GetAllAsync()).Select(entity => entity.ToResponse()).ToList();

        public async Task<PickupLocationResponse?> GetByIdAsync(Guid id, Guid userId)
        {
            var entity = await _pickupLocationRepo.GetByIdAsync(id);
            if (entity is null || entity.UserId != userId)
                throw new UnauthorizedAccessException("The pickup location does not belong to the current user");

            return entity.ToResponse();
        }

        public async Task<PickupLocationResponse?> UpdateAsync(PickupLocationUpdateRequest? updateDto, Guid userId)
        {
            _logger.LogInformation($"{GetType().Name} - UpadateAsync Has been reached");

            if (updateDto is null)
            {
                _logger.LogError($"{GetType().Name} - Empty updateDto provided in CreateAsync");
                throw new ArgumentNullException(ExceptionMessages.EmptyUpdateRequest);
            }

            var existing = await _pickupLocationRepo.GetByIdAsync(updateDto.Id);
            if (existing is null || existing.UserId != userId)
                throw new UnauthorizedAccessException("The pickup location does not belong to the current user");

            var updatedrequest = updateDto.ToEntity();

            return (await _pickupLocationRepo.Update(updatedrequest)).ToResponse();

        }
        public async Task<PickupLocationResponse?> CreateAsync(PickupLocationAddRequest? createDto, Guid userId)
        {
            _logger.LogInformation($"{GetType().Name} - CreateAsync Has been reached");


            if (createDto is null)
            {
                _logger.LogError($"{GetType().Name} - Empty createDto provided in CreateAsync");
                throw new ArgumentNullException(ExceptionMessages.EmptyAddRequest);
            }

            if (createDto.Lon == 0 || createDto.Lat == 0)
            {
                _logger.LogError($"{GetType().Name} - Empty Coordination for the location provided in CreateAsync");
                throw new ArgumentException(ExceptionMessages.NoCoordinationFound);
            }

            var userLocations = await _pickupLocationRepo.GetByUserId(userId);
            if (userLocations.Select(x => x.ToResponse()).ToList().Any(x => x.Equals(createDto)))
            {
                _logger.LogError($"{GetType().Name} - Empty Coordination for the location provided in CreateAsync");
                throw new ArgumentException("An Identical location for the user Already saved");
            }

            var entity = createDto.ToEntity();
            entity.Id = Guid.NewGuid();
            entity.UserId = userId;
            await _pickupLocationRepo.AddAsync(entity);
            return entity.ToResponse();
        }

        public async Task<bool> SoftDeleteLocation(Guid id, Guid userId)
        {
            _logger.LogInformation($"{GetType().Name} - SoftDeleteLocation Has been reached");


            if (id == Guid.Empty)
            {
                _logger.LogError($"{GetType().Name} - Empty LocationId provided in SoftDeleteLocation");
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }
            var location = await _pickupLocationRepo.GetByIdAsync(id);
            if (location is null)
            {
                _logger.LogError($"{GetType().Name} - No Location founded with Id {id}", id);
                throw new ArgumentException(ExceptionMessages.NoLocationFound);
            }

            if (location.UserId != userId)
                throw new UnauthorizedAccessException("The pickup location does not belong to the current user");

            return await _pickupLocationRepo.SoftDeleteLocation(id);
        }

    }
}
