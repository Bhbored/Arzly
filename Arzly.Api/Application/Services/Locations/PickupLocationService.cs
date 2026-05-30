using Arzly.Api.Application.Contracts.Locations;
using Arzly.Api.Domain.Contracts.Locations;
using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Contracts.Locations;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Mappings;
using Arzly.Shared.Constants;
using Arzly.Shared.DTOs.Request.PickupLocation;
using Arzly.Shared.DTOs.Response.PickupLocation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Arzly.Api.Application.Services.Locations
{
    public class PickupLocationService : BaseService<PickupLocation, PickupLocationResponse, PickupLocationAddRequest, PickupLocationUpdateRequest, Guid>,
        IPickupLocationService
    {
        private readonly IPickupLocationRepository _pickupLocationRepo;
        private readonly ILogger<PickupLocationService> _logger;
        public PickupLocationService(IPickupLocationRepository repository,
            ILogger<PickupLocationService> logger) : base(repository)
        {
            _pickupLocationRepo = repository;
            _logger = logger;
        }

        public async Task<List<PickupLocationResponse>> GetByUserId(Guid? userId)
        {
            _logger.LogInformation($"{GetType().Name} - GetByUserId Has been reached");

            var entities = await _pickupLocationRepo.GetByUserId(userId ?? Guid.Empty);

            return entities.Select(x => MapToDto(x)).ToList();
        }

        public override async Task<PickupLocationResponse?> UpdateAsync(PickupLocationUpdateRequest? updateDto, string? userId)
        {
            _logger.LogInformation($"{GetType().Name} - UpadateAsync Has been reached");

            if (updateDto is null)
            {
                _logger.LogError($"{GetType().Name} - Empty updateDto provided in CreateAsync");
                throw new ArgumentNullException(ExceptionMessages.EmptyUpdateRequest);
            }

            var updatedrequest = MapToEntity(updateDto);

            return (await _pickupLocationRepo.Update(updatedrequest)).ToResponse();

        }
        public override async Task<PickupLocationResponse?> CreateAsync(PickupLocationAddRequest? createDto, string? userId)
        {
            _logger.LogInformation($"{GetType().Name} - CreateAsync Has been reached");


            if (createDto is null)
            {
                _logger.LogError($"{GetType().Name} - Empty createDto provided in CreateAsync");
                throw new ArgumentNullException(ExceptionMessages.EmptyAddRequest);
            }

            //comment this for now till the location service is done
            if (createDto.Lon == 0 || createDto.Lat == 0)
            {
                _logger.LogError($"{GetType().Name} - Empty Coordination for the location provided in CreateAsync");
                throw new ArgumentException(ExceptionMessages.NoCoordinationFound);
            }

            var userLocations = await _pickupLocationRepo.GetByUserId(userId != null ? Guid.Parse(userId) : Guid.Empty);
            if (userLocations.Select(x => x.ToResponse()).ToList().Any(x => x.Equals(createDto)))
            {
                _logger.LogError($"{GetType().Name} - Empty Coordination for the location provided in CreateAsync");
                throw new ArgumentException("An Identical location for the user Already saved");
            }

            var entity = MapToEntity(createDto);
            entity.Id = Guid.NewGuid();
            // TODO: set UserId from authenticated user
            await _pickupLocationRepo.AddAsync(entity);
            return MapToDto(entity);
        }

        public async Task<bool> SoftDeleteLocation(Guid id, string? userId)
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

            return await _pickupLocationRepo.SoftDeleteLocation(id);
        }

        protected override PickupLocationResponse MapToDto(PickupLocation entity) => entity.ToResponse();
        protected override PickupLocation MapToEntity(PickupLocationAddRequest createDto) => createDto.ToEntity();
        protected override PickupLocation MapToEntity(PickupLocationUpdateRequest updateDto) => updateDto.ToEntity();


    }
}
