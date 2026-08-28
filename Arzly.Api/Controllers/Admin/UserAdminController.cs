using Arzly.Api.Application.Contracts.Users;
using Arzly.Shared.DTOs.Request.UserModeration;
using Arzly.Shared.DTOs.Response.UserModeration;
using Arzly.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Arzly.Api.Controllers.Admin;

[Route("arzly/v{version:apiVersion}/admin/users")]
[ApiController]
[Authorize(Roles = "admin,support")]
[EnableRateLimiting("writes")]
public class UserAdminController : ControllerBase
{
    private readonly IUserModerationService _service;

    public UserAdminController(IUserModerationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ModeratedUserResponse>>> GetAll(
        [FromQuery] string? search,
        [FromQuery] int pageSize = 20,
        [FromQuery] int currentPage = 0) =>
        Ok(await _service.GetUsersAsync(search, pageSize, currentPage));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ModeratedUserResponse>> GetById(Guid id) =>
        Ok(await _service.GetByIdAsync(id));

    [HttpPut("{id:guid}/ban")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<ModeratedUserResponse>> Ban(
        Guid id,
        [FromBody] BanUserRequest request) =>
        Ok(await _service.BanAsync(id, User.GetUserId(), request.Reason, request.ExpiresAt));

    [HttpPut("{id:guid}/unban")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<ModeratedUserResponse>> Unban(Guid id) =>
        Ok(await _service.UnbanAsync(id, User.GetUserId()));

    [HttpPut("{id:guid}/role")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<ModeratedUserResponse>> ChangeRole(
        Guid id,
        [FromBody] ChangeUserRoleRequest request) =>
        Ok(await _service.ChangeRoleAsync(id, User.GetUserId(), request.Role));
}
