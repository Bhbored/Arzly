using Arzly.Api.Application.Contracts.Communications;
using Arzly.Shared.DTOs.Request.Notification;
using Arzly.Shared.DTOs.Response.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Arzly.Shared.Extensions;
using Microsoft.AspNetCore.RateLimiting;

namespace Arzly.Api.Controllers.Admin;

[Route("arzly/v{version:apiVersion}/admin/notifications")]
[ApiController]
[Authorize(Roles = "admin")]
public class NotificationAdminController : ControllerBase
{
    private readonly INotificationService _service;

    public NotificationAdminController(INotificationService service)
    {
        _service = service;
    }

    [HttpPost("targeted")]
    [EnableRateLimiting("broadcasts")]
    public async Task<ActionResult<NotificationResponse>> SendTargeted(
        [FromBody] NotificationAddRequest request)
    {
        var result = await _service.SendTargetedAsync(request, User.GetUserId());
        return Created(string.Empty, result);
    }

    [HttpPost("broadcast")]
    [EnableRateLimiting("broadcasts")]
    public async Task<ActionResult> Broadcast([FromBody] NotificationAddRequest request)
    {
        var delivered = await _service.BroadcastAsync(request, User.GetUserId());
        return Ok(new { Delivered = delivered });
    }
}
