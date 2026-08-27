using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Arzly.Tests.Helpers
{
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string SchemeName = "TestScheme";
        public const string AuthenticationHeader = "X-Test-Authentication";
        public const string RoleHeader = "X-Test-Role";
        public const string UserIdHeader = "X-Test-User-Id";
        public static readonly Guid DefaultUserId = Guid.Parse("10000000-0000-0000-0000-000000000001");

        public TestAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder)
            : base(options, logger, encoder)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Headers.TryGetValue(AuthenticationHeader, out var authentication) &&
                string.Equals(authentication, "anonymous", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var role = Request.Headers.TryGetValue(RoleHeader, out var requestedRole)
                ? requestedRole.ToString()
                : "user";
            var userId = Request.Headers.TryGetValue(UserIdHeader, out var requestedUserId)
                ? Guid.Parse(requestedUserId.ToString())
                : DefaultUserId;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, "testuser"),
                new Claim(ClaimTypes.Role, role),
                new Claim("sub", userId.ToString())
            };

            var identity = new ClaimsIdentity(claims, SchemeName);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, SchemeName);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
