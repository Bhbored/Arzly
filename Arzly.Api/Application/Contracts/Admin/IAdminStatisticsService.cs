using Arzly.Shared.DTOs.Response.Admin;

namespace Arzly.Api.Application.Contracts.Admin;

public interface IAdminStatisticsService
{
    Task<OperationalStatisticsResponse> GetAsync();
}
