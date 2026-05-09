using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Mappings;
using Arzly.Shared.Constants;
using Arzly.Shared.DTOs.Request.PickupLocation;
using Arzly.Shared.DTOs.Response.PickupLocation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Arzly.Api.Application.Services
{
    public class PickupLocationService : BaseService<PickupLocation, PickupLocationResponse, PickupLocationAddRequest, PickupLocationUpdateRequest, Guid>, IPickupLocationService
    {
        private readonly IPickupLocationRepository _pickupLocationRepo;
        private readonly ILogger<PickupLocationService> _logger;
        private readonly IUserService _userService;
        public PickupLocationService(IPickupLocationRepository repository,
            ILogger<PickupLocationService> logger, IUserService userService) : base(repository)
        {
            _pickupLocationRepo = repository;
            _userService = userService;
            _logger = logger;
        }

        public async Task<List<PickupLocationResponse>> GetByUserId(string? userId)
        {
            _logger.LogInformation($"{GetType().Name} - GetByUserId Has been reached");

            if (string.IsNullOrWhiteSpace(userId))
            {
                _logger.LogError($"{GetType().Name} - Empty userId provided in GetByUserId");
                throw new ArgumentNullException(ExceptionMessages.MissingFirebaseId);
            }

            var user = await _userService.GetByFireBaseIdAsync(userId);

            var entities = await _pickupLocationRepo.GetByUserId(user!.Id);

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

            await _userService.GetByFireBaseIdAsync(userId);
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
            //if (createDto.Lon == 0 || createDto.Lat == 0)
            //{
            //    _logger.LogError($"{GetType().Name} - Empty Coordination for the location provided in CreateAsync");
            //    throw new ArgumentException(ExceptionMessages.NoCoordinationFound);
            //}
            var user = await _userService.GetByFireBaseIdAsync(userId);

            var userLocations = await _pickupLocationRepo.GetByUserId(user!.Id);
            if (userLocations.Select(x => x.ToResponse()).ToList().Any(x => x.Equals(createDto)))
            {
                _logger.LogError($"{GetType().Name} - Empty Coordination for the location provided in CreateAsync");
                throw new ArgumentException("An Identical location for the user Already saved");
            }

            var entity = MapToEntity(createDto);
            entity.Id = Guid.NewGuid();
            entity.UserId = user.Id;
            await _pickupLocationRepo.AddAsync(entity);
            return MapToDto(entity);
        }


        protected override PickupLocationResponse MapToDto(PickupLocation entity) => entity.ToResponse();
        protected override PickupLocation MapToEntity(PickupLocationAddRequest createDto) => createDto.ToEntity();
        protected override PickupLocation MapToEntity(PickupLocationUpdateRequest updateDto) => updateDto.ToEntity();
    }
}
