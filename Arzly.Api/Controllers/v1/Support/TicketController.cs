using Arzly.Api.Application.Contracts.Users;
using Arzly.Api.Application.Contracts.Support;
using Arzly.Shared.DTOs.Request.Ticket;
using Arzly.Shared.DTOs.Request.TicketAttachment;
using Arzly.Shared.DTOs.Request.TicketMessage;
using Arzly.Shared.DTOs.Response.Ticket;
using Arzly.Shared.DTOs.Response.TicketAttachment;
using Arzly.Shared.DTOs.Response.TicketMessage;
using Arzly.Shared.Enums.Ticket;
using Arzly.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers.v1.Support;

public class TicketController : CustomeControllerBase
{
    private readonly ITicketService _service;

    public TicketController(ITicketService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<TicketResponse>> Create([FromBody] TicketAddRequest request)
    {
        var result = await _service.CreateAsync(request, User.GetUserId());
        return CreatedAtAction(nameof(GetConversation), new { id = result!.Id }, result);
    }

    [HttpGet("mine")]
    public async Task<ActionResult<List<TicketResponse>>> GetMine(
        [FromQuery] int pageSize = 20,
        [FromQuery] int currentPage = 0) =>
        Ok(await _service.GetUserTicketsAsync(User.GetUserId(), pageSize, currentPage));

    [HttpGet("queue")]
    [Authorize(Roles = "admin,support")]
    public async Task<ActionResult<List<TicketResponse>>> GetQueue(
        [FromQuery] int pageSize = 20,
        [FromQuery] int currentPage = 0) =>
        Ok(await _service.GetQueueAsync(pageSize, currentPage));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TicketConversationResponse>> GetConversation(Guid id) =>
        Ok(await _service.GetConversationAsync(id, User.GetUserId(), IsStaff()));

    [HttpPut("{id:guid}/status")]
    [Authorize(Roles = "admin,support")]
    public async Task<ActionResult<TicketResponse>> SetStatus(Guid id, [FromBody] TicketStatus status) =>
        Ok(await _service.SetStatusAsync(id, User.GetUserId(), status));

    [HttpPost("{id:guid}/messages")]
    public async Task<ActionResult<TicketMessageResponse>> AddMessage(
        Guid id,
        [FromBody] TicketMessageAddRequest request)
    {
        if (request.TicketId != id)
            return BadRequest("Route id must match ticket id");
        var result = await _service.AddMessageAsync(request, User.GetUserId(), IsStaff());
        return CreatedAtAction(nameof(GetConversation), new { id }, result);
    }

    [HttpPost("{id:guid}/attachments")]
    public async Task<ActionResult<TicketAttachmentResponse>> AddAttachment(
        Guid id,
        [FromBody] TicketAttachmentAddRequest request)
    {
        if (request.TicketId != id)
            return BadRequest("Route id must match ticket id");
        var result = await _service.AddAttachmentAsync(request, User.GetUserId(), IsStaff());
        return CreatedAtAction(nameof(GetConversation), new { id }, result);
    }

    private bool IsStaff() => User.IsInRole("admin") || User.IsInRole("support");
}
