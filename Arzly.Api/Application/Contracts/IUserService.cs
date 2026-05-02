using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.DTOs.Request.User;
using Arzly.Shared.DTOs.Response.User;

namespace Arzly.Api.Application.Contracts
{
    public interface IUserService : IBaseService<AppUser, AppUserResponse, AppUserAddRequest, AppUserUpdateRequest, string>
    {
        Task<AppUserResponse?> GetByFireBaseIdAsync(string? id);
    }
}
