using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.UserReport;
using Arzly.Shared.DTOs.Response.UserReport;

namespace Arzly.Api.Application.Contracts
{
    public interface IUserReportService : IBaseService<UserReport, UserReportResponse, UserReportAddRequest, UserReportUpdateRequest, Guid>
    {
    }
}
