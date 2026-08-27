using Arzly.Shared.DTOs.Response.UserModeration;

namespace Arzly.Api.Application.Contracts.Users;

public interface IUserModerationService
{
    Task<List<ModeratedUserResponse>> GetUsersAsync(string? search, int pageSize, int currentPage);
    Task<ModeratedUserResponse> GetByIdAsync(Guid id);
    Task<ModeratedUserResponse> BanAsync(Guid id, Guid actorId, string reason, DateTime? expiresAt);
    Task<ModeratedUserResponse> UnbanAsync(Guid id, Guid actorId);
    Task<ModeratedUserResponse> ChangeRoleAsync(Guid id, Guid actorId, string role);
}
