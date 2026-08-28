using Arzly.Api.Domain.Entities.Users;
using Arzly.Shared.DTOs.Request.UserReport;
using Arzly.Shared.DTOs.Response.UserReport;

namespace Arzly.Api.Application.Contracts.Users
{
    public interface IUserReportService
    {
        Task<List<UserReportResponse>> GetAllAsync();
        Task<UserReportResponse?> CreateAsync(UserReportAddRequest? request, Guid userId);
        Task<bool> DeleteAsync(Guid id);
        Task<UserReportResponse> GetByIdAsync(Guid id, Guid userId, bool canModerate);
        Task<UserReportResponse> ResolveAsync(Guid id, Guid resolverId, bool isResolved);
    }
}
