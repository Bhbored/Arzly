using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Hubs.Contracts;
using Arzly.Api.Infrastructure.Identity;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using MimeKit;
using System.Text;

namespace Arzly.Api.Hubs.Services
{
    public class EmailService : IEmailService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly ILogger<EmailService> _logger;

        public EmailService(UserManager<ApplicationUser> userManager, IConfiguration configuration
            , IUserProfileRepository userProfileRepository, ILogger<EmailService> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _userProfileRepository = userProfileRepository;
            _logger = logger;
        }

        public async Task SendEmailVerificationAsync(string userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return;
            if (user.EmailConfirmed) return;

            var code = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            var body = $"""
            <h1>Welcome to Arzly!</h1>
            <p>Your verification code is:</p>
            <h2>{code}</h2>
            <p>This code expires in 10 minutes.</p>
            """;

            await SendAsync(user.Email!, "Your Arzly verification code", body, cancellationToken);
        }

        public async Task<IdentityResult> ConfirmEmailWithCodeAsync(string userId, string code, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", code);
            if (!isValid)
                return IdentityResult.Failed(new IdentityError { Description = "Invalid or expired code." });

            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);


            var oldProfile = await _userProfileRepository.GetByIdAsync(user.Id);
            if (oldProfile != null)
            {
                oldProfile.IsVerified = true;
                await _userProfileRepository.Update(oldProfile);
            }

            return IdentityResult.Success;
        }

        public async Task SendPasswordResetAsync(string email, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null || !user.EmailConfirmed) return;

            var code = await _userManager.GenerateTwoFactorTokenAsync(user, "PasswordReset");

            var body = $"""
            <h1>Arzly Password Reset</h1>
            <p>Your password reset code is:</p>
            <h2>{code}</h2>
            <p>This code expires in 10 minutes.</p>
            <p>If you didn't request this, ignore this email.</p>
            """;

            await SendAsync(email, "Reset your Arzly password", body, cancellationToken);
        }

        public async Task<IdentityResult> ResetPasswordAsync(string email, string code, string newPassword, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return IdentityResult.Failed(new IdentityError { Description = "Invalid request." });

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "PasswordReset", code);
            if (!isValid)
                return IdentityResult.Failed(new IdentityError { Description = "Invalid or expired code." });

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            return await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
        }

        private async Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Arzly", _configuration["Email:Username"]!));
            email.To.Add(new MailboxAddress("", to));
            email.Subject = subject;
            email.Body = new TextPart("html") { Text = body };

            using var smtp = new SmtpClient
            {
                Timeout = _configuration.GetValue<int?>("Email:TimeoutMilliseconds") ?? 15000
            };
            try
            {
                await smtp.ConnectAsync(
                    _configuration["Email:Host"]!,
                    int.Parse(_configuration["Email:Port"]!),
                    SecureSocketOptions.StartTls,
                    cancellationToken);

                await smtp.AuthenticateAsync(
                    _configuration["Email:Username"]!,
                    _configuration["Email:Password"]!,
                    cancellationToken);

                await smtp.SendAsync(email, cancellationToken);
                await smtp.DisconnectAsync(true, cancellationToken);
                _logger.LogInformation("Email sent. Subject: {Subject}", subject);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to send email. Subject: {Subject}", subject);
                throw;
            }
        }
    }
}
