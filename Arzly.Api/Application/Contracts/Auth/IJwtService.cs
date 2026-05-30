using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.DTOs.Response.Auth;
using System.Security.Claims;

namespace Arzly.Api.Application.Contracts.Auth
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user, string role);
        ClaimsPrincipal? GetPrincipleFromJwtToken(string? token);
    }
}
