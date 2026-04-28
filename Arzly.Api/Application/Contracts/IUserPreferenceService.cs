using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.UserPreference;
using Arzly.Shared.DTOs.Response.UserPreference;

namespace Arzly.Api.Application.Contracts
{
    public interface IUserPreferenceService
    {
        Task<UserPreferenceResponse?> GetByIdAsync(string id);
        Task<UserPreferenceResponse?> UpdateAsync(UserPreferenceUpdateRequest request);
    }
}
