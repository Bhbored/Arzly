using Arzly.Api.Hubs.Contracts;
using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.DTOs.Request.Email;
using Arzly.Shared.Extensions;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Security.Claims;

namespace Arzly.Api.Controllers.v1.Auth
{
    public class EmailController : CustomeControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-verification")]
        [Authorize(Roles ="user")]
        public async Task<IActionResult> SendVerificationEmail()
        {
            var userId = User.GetUserId();
            if (userId == Guid.Empty)
                return Unauthorized();

            await _emailService.SendEmailVerificationAsync(userId.ToString());
            return Ok(new { message = "Verification code sent to your email." });
        }

        [HttpPost("verify-email")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
        {
            var userId = User.GetUserId();
            if (userId == Guid.Empty)
                return Unauthorized();

            var result = await _emailService.ConfirmEmailWithCodeAsync(userId.ToString(), request.Code);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Email verified successfully." });
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            await _emailService.SendPasswordResetAsync(request.Email);
            return Ok(new { message = "If the email exists, a reset code has been sent." });
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _emailService.ResetPasswordAsync(request.Email, request.Code, request.NewPassword);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Password reset successful." });
        }
    }
}
