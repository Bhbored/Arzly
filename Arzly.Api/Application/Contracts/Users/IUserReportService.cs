using Arzly.Api.Domain.Entities.Users;
using Arzly.Shared.DTOs.Request.UserReport;
using Arzly.Shared.DTOs.Response.UserReport;

namespace Arzly.Api.Application.Contracts.Users
{
    public interface IUserReportService : IBaseService<UserReport, UserReportResponse, UserReportAddRequest, UserReportUpdateRequest, Guid>
    {
        Task<UserReportResponse> GetByIdAsync(Guid id, Guid userId, bool canModerate);
        Task<UserReportResponse> ResolveAsync(Guid id, Guid resolverId, bool isResolved);
    }
}
