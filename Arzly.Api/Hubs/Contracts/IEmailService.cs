using Arzly.Api.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Arzly.Api.Hubs.Contracts
{
    public interface IEmailService
    {
        Task SendEmailVerificationAsync(string userId, CancellationToken cancellationToken = default);
        Task<IdentityResult> ConfirmEmailWithCodeAsync(string userId, string code, CancellationToken cancellationToken = default);
        Task SendPasswordResetAsync(string email, CancellationToken cancellationToken = default);
        Task<IdentityResult> ResetPasswordAsync(string email, string code, string newPassword, CancellationToken cancellationToken = default);
    }
}
