using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Mappings;
using Arzly.Shared.Constants;
using Arzly.Shared.DTOs.Request.PickupLocation;
using Arzly.Shared.DTOs.Response.PickupLocation;

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


        public override async Task<PickupLocationResponse?> CreateAsync(PickupLocationAddRequest? createDto, string? userId)
        {
            _logger.LogInformation($"{GetType().Name} - CreateAsync Has been reached");

            await _userService.GetByFireBaseIdAsync(userId);

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

            var userLocations = await _pickupLocationRepo.GetByUserId(userId);
            if (userLocations.Select(x => x.ToResponse()).ToList().Any(x => x.Equals(createDto)))
            {
                _logger.LogError($"{GetType().Name} - Empty Coordination for the location provided in CreateAsync");
                throw new ArgumentException("An Identical location for the user Already saved");
            }

            var entity = MapToEntity(createDto);
            entity.Id = Guid.NewGuid();
            await _pickupLocationRepo.AddAsync(entity);
            return MapToDto(entity);
        }
        protected override PickupLocationResponse MapToDto(PickupLocation entity) => entity.ToResponse();
        protected override PickupLocation MapToEntity(PickupLocationAddRequest createDto) => createDto.ToEntity();
        protected override PickupLocation MapToEntity(PickupLocationUpdateRequest updateDto) => updateDto.ToEntity();
    }
}
