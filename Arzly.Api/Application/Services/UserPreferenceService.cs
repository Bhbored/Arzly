using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.UserPreference;
using Arzly.Shared.DTOs.Response.UserPreference;

namespace Arzly.Api.Application.Services
{
    public class UserPreferenceService : IUserPreferenceService
    {
        private readonly IUserPreferenceRepository _repository;

        public UserPreferenceService(IUserPreferenceRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserPreferenceResponse?> GetByIdAsync(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity?.ToResponse();
        }

        public async Task<UserPreferenceResponse?> UpdateAsync(UserPreferenceUpdateRequest request)
        {
            var entity = request.ToEntity();
            var updatedEntity = await _repository.Update(entity);
            return updatedEntity.ToResponse();
        }
    }
}
