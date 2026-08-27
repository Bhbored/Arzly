using Arzly.Api.Application.Contracts.Admin;
using Arzly.Shared.DTOs.Response.UserActivityLog;
using Arzly.Shared.Enums.Activity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers.Admin;

[Route("arzly/v{version:apiVersion}/admin/audit")]
[ApiController]
[Authorize(Roles = "admin")]
public class AuditAdminController : ControllerBase
{
    private readonly IAdminAuditService _service;

    public AuditAdminController(IAdminAuditService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserActivityLogResponse>>> Get(
        [FromQuery] ActivityActionType? actionType,
        [FromQuery] ActivityTargetType? targetType,
        [FromQuery] Guid? actorId,
        [FromQuery] int pageSize = 50,
        [FromQuery] int currentPage = 0) =>
        Ok(await _service.GetAsync(actionType, targetType, actorId, pageSize, currentPage));
}
