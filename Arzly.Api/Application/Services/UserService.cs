using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Infrastructure.Identity;
using Arzly.Api.Mappings;
using Arzly.Shared.Constants;
using Arzly.Shared.DTOs.Request.User;
using Arzly.Shared.DTOs.Response.User;

namespace Arzly.Api.Application.Services
{
    public class UserService : BaseService<AppUser, AppUserResponse, AppUserAddRequest, AppUserUpdateRequest, string>, IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _UserRepository;
        public UserService(IUserRepository repository, ILogger<UserService> logger) : base(repository)
        {
            _logger = logger;
            _UserRepository = repository;
        }

        public async Task<AppUserResponse?> GetByFireBaseIdAsync(string? id)
        {
            _logger.LogInformation($"Reached {GetType().Name} - GetByFireBaseIdAsync");
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogError($"{GetType().Name} - GetByFireBaseIdAsync Error: {ExceptionMessages.MissingId} ");
                throw new ArgumentException(ExceptionMessages.MissingId);
            }

            var entity = await _UserRepository.GetByFireBaseIdAsync(id);
            if (entity is null || entity == default)
            {
                _logger.LogError($"{GetType().Name} - GetByFireBaseIdAsync Error:" +
                    $" {ExceptionMessages.InvalidFirebaseId} ");

                throw new ArgumentNullException(ExceptionMessages.InvalidFirebaseId);
            }
            return MapToDto(entity);
        }

        protected override AppUserResponse MapToDto(AppUser entity) => entity.ToResponse();
        protected override AppUser MapToEntity(AppUserAddRequest createDto) => createDto.ToEntity();
        protected override AppUser MapToEntity(AppUserUpdateRequest updateDto) => updateDto.ToEntity();
    }
}
