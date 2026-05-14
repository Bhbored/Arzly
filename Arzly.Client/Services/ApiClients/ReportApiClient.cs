using Arzly.Client.Services.Contracts;
using Arzly.Shared.DTOs.Response.UserReport;

namespace Arzly.Client.Services.ApiClients;

public class ReportApiClient : BaseApiClient,  IReportApiClient
{
    public ReportApiClient(HttpClient httpClient, ILogger<ReportApiClient> logger) 
        : base(httpClient, logger)
    {
    }

    public async Task<List<UserReportResponse>> GetAllReportsAsync()
    {
        var result = await GetAsync<List<UserReportResponse>>("arzly/UserReport");
        return result ?? new List<UserReportResponse>();
    }

    public async Task<UserReportResponse?> GetReportByIdAsync(Guid id)
    {
        return await GetAsync<UserReportResponse>($"arzly/UserReport/{id}");
    }
}
