using Arzly.Api.Application.Contracts.Users;
using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Domain.Entities.Users;
using Arzly.Api.Mappings;
using Arzly.Shared.Constants;
using Arzly.Shared.DTOs.Request.UserProfile;
using Arzly.Shared.DTOs.Response.UserProfile;

namespace Arzly.Api.Application.Services.Users
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _repository;

        public UserProfileService(IUserProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserProfileResponse?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(ExceptionMessages.MissingId);

            var entity = await _repository.GetByIdAsync(id);
            if (entity is null)
                throw new ArgumentException($"No UserProfile with ID {id} Found");

            return entity.ToResponse();
        }

        public async Task<UserProfileResponse?> UpdateAsync(UserProfileUpdateRequest updateDto, Guid userId)
        {
            if (updateDto == null)
                throw new ArgumentNullException(ExceptionMessages.EmptyUpdateRequest);

            if (updateDto.UserId != userId)
                throw new UnauthorizedAccessException("A user can only update their own profile");

            var entity = updateDto.ToEntity();
            entity.UserId = userId;
            var updatedEntity = await _repository.Update(entity);
            return updatedEntity.ToResponse();
        }
    }
}
