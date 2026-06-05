using Arzly.Api.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Arzly.Api.Hubs.Contracts
{
    public interface IEmailService
    {
        Task SendEmailVerificationAsync(string userId);
        Task<IdentityResult> ConfirmEmailWithCodeAsync(string userId, string code);
        Task SendPasswordResetAsync(string email);
        Task<IdentityResult> ResetPasswordAsync(string email, string code, string newPassword);
    }
}
