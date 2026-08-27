using Arzly.Api.Application.Contracts.Admin;
using Arzly.Shared.DTOs.Response.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers.Admin;

[Route("arzly/v{version:apiVersion}/admin/operations")]
[ApiController]
[Authorize(Roles = "admin")]
public class OperationsAdminController : ControllerBase
{
    private readonly IAdminStatisticsService _service;

    public OperationsAdminController(IAdminStatisticsService service)
    {
        _service = service;
    }

    [HttpGet("statistics")]
    public async Task<ActionResult<OperationalStatisticsResponse>> GetStatistics() =>
        Ok(await _service.GetAsync());
}
