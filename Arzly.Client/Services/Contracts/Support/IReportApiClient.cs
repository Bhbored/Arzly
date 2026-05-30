using Arzly.Shared.DTOs.Response.UserReport;

namespace Arzly.Client.Services.Contracts.Support;

public interface IReportApiClient
{
    Task<List<UserReportResponse>> GetAllReportsAsync();
    Task<UserReportResponse?> GetReportByIdAsync(Guid id);
}
