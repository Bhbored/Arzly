using Arzly.Api.Application.Contracts.Users;
using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.DTOs.Response.UserModeration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Domain.Entities.Users;
using Arzly.Shared.Enums.Activity;

namespace Arzly.Api.Application.Services.Users;

public class UserModerationService : IUserModerationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IUserActivityLogRepository _activityLogs;

    public UserModerationService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IUserActivityLogRepository activityLogs)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _activityLogs = activityLogs;
    }

    public async Task<List<ModeratedUserResponse>> GetUsersAsync(
        string? search,
        int pageSize,
        int currentPage)
    {
        pageSize = Math.Clamp(pageSize, 1, 100);
        currentPage = Math.Max(currentPage, 0);
        var query = _userManager.Users.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(x =>
                (x.Email != null && x.Email.Contains(search)) ||
                (x.PhoneNumber != null && x.PhoneNumber.Contains(search)));
        return await query.OrderByDescending(x => x.CreatedAt)
            .Skip(currentPage * pageSize).Take(pageSize)
            .Select(x => new ModeratedUserResponse
            {
                Id = x.Id, Email = x.Email, PhoneNumber = x.PhoneNumber,
                IsBanned = x.IsBanned, BanReason = x.BanReason,
                BanExpiresAt = x.BanExpiresAt, CreatedAt = x.CreatedAt
            }).ToListAsync();
    }

    public async Task<ModeratedUserResponse> GetByIdAsync(Guid id)
    {
        var user = await FindUser(id);
        var response = ToResponse(user);
        response.Roles = (await _userManager.GetRolesAsync(user)).ToList();
        return response;
    }

    public async Task<ModeratedUserResponse> BanAsync(
        Guid id,
        Guid actorId,
        string reason,
        DateTime? expiresAt)
    {
        if (id == actorId)
            throw new ArgumentException("Administrators cannot ban their own account");
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("A ban reason is required");
        if (expiresAt is not null && expiresAt <= DateTime.UtcNow)
            throw new ArgumentException("Ban expiration must be in the future");

        var user = await FindUser(id);
        user.IsBanned = true;
        user.BanReason = reason.Trim();
        user.BanExpiresAt = expiresAt?.ToUniversalTime();
        user.RefreshToken = null;
        user.RefreshTokenExpirateDate = null;
        await EnsureUpdated(user);
        await AddAudit(actorId, id, ActivityActionType.UserBanned, reason.Trim());
        return ToResponse(user);
    }

    public async Task<ModeratedUserResponse> UnbanAsync(Guid id, Guid actorId)
    {
        var user = await FindUser(id);
        user.IsBanned = false;
        user.BanReason = null;
        user.BanExpiresAt = null;
        await EnsureUpdated(user);
        await AddAudit(actorId, id, ActivityActionType.UserUnbanned, "User unbanned");
        return ToResponse(user);
    }

    public async Task<ModeratedUserResponse> ChangeRoleAsync(Guid id, Guid actorId, string role)
    {
        if (id == actorId)
            throw new ArgumentException("Administrators cannot change their own role");
        var normalizedRole = role.Trim().ToLowerInvariant();
        if (normalizedRole is not ("admin" or "support" or "user"))
            throw new ArgumentException("Role must be admin, support, or user");
        if (!await _roleManager.RoleExistsAsync(normalizedRole))
            throw new ArgumentException("Role does not exist");

        var user = await FindUser(id);
        var currentRoles = await _userManager.GetRolesAsync(user);
        if (currentRoles.Contains("admin", StringComparer.OrdinalIgnoreCase) && normalizedRole != "admin")
        {
            var admins = await _userManager.GetUsersInRoleAsync("admin");
            if (admins.Count <= 1)
                throw new ArgumentException("The final administrator cannot be demoted");
        }

        if (!currentRoles.Contains(normalizedRole, StringComparer.OrdinalIgnoreCase))
        {
            var added = await _userManager.AddToRoleAsync(user, normalizedRole);
            if (!added.Succeeded)
                throw new InvalidOperationException(string.Join(" | ", added.Errors.Select(x => x.Description)));
        }
        var rolesToRemove = currentRoles.Where(x => !x.Equals(normalizedRole, StringComparison.OrdinalIgnoreCase)).ToList();
        if (rolesToRemove.Count > 0)
        {
            var removed = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            if (!removed.Succeeded)
                throw new InvalidOperationException(string.Join(" | ", removed.Errors.Select(x => x.Description)));
        }

        await AddAudit(actorId, id, ActivityActionType.UserRoleChanged,
            $"Role changed to {normalizedRole}");
        var response = ToResponse(user);
        response.Roles = [normalizedRole];
        return response;
    }

    private Task AddAudit(Guid actorId, Guid targetId, ActivityActionType action, string details) =>
        _activityLogs.AddAsync(new UserActivityLog
        {
            ActorId = actorId, ActorRole = "admin", ActionType = action,
            TargetType = ActivityTargetType.User, TargetId = targetId.ToString(),
            Details = details, Timestamp = DateTime.UtcNow, IsSuccess = true
        });

    private async Task<ApplicationUser> FindUser(Guid id) =>
        await _userManager.FindByIdAsync(id.ToString())
        ?? throw new ArgumentException("User not found");

    private async Task EnsureUpdated(ApplicationUser user)
    {
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new InvalidOperationException(string.Join(" | ", result.Errors.Select(x => x.Description)));
    }

    private static ModeratedUserResponse ToResponse(ApplicationUser user) => new()
    {
        Id = user.Id,
        Email = user.Email,
        PhoneNumber = user.PhoneNumber,
        IsBanned = user.IsBanned,
        BanReason = user.BanReason,
        BanExpiresAt = user.BanExpiresAt,
        CreatedAt = user.CreatedAt
    };
}
