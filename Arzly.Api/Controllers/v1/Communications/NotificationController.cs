using Arzly.Api.Application.Contracts.Communications;
using Arzly.Shared.DTOs.Response.Notification;
using Arzly.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers.v1.Communications;

public class NotificationController : CustomeControllerBase
{
    private readonly INotificationService _service;

    public NotificationController(INotificationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<NotificationResponse>>> GetInbox(
        [FromQuery] bool? isRead,
        [FromQuery] int pageSize = 20,
        [FromQuery] int currentPage = 0) =>
        Ok(await _service.GetInboxAsync(User.GetUserId(), isRead, pageSize, currentPage));

    [HttpPut("{id:guid}/read")]
    public async Task<ActionResult<NotificationResponse>> MarkRead(Guid id) =>
        Ok(await _service.MarkReadAsync(id, User.GetUserId()));
}
