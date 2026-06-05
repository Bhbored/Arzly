using Arzly.Shared.DTOs.Request.UserProfile;
using Arzly.Shared.DTOs.Response.UserProfile;

namespace Arzly.Api.Application.Contracts.Users
{
    public interface IUserProfileService
    {
        Task<UserProfileResponse?> GetByIdAsync(Guid id);
        Task<UserProfileResponse?> UpdateAsync(UserProfileUpdateRequest updateDto, Guid userId);
    }
}
