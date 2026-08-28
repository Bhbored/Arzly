using Arzly.Api.Application.Contracts.Auth;
using Arzly.Shared.DTOs.Request.Auth;
using Arzly.Shared.DTOs.Response.Auth;
using Arzly.Shared.Enums;
using Arzly.Shared.Extensions;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace Arzly.Api.Controllers.v1.Auth
{
    public class AuthenticationController : CustomeControllerBase
    {

        private readonly IAuthService _authService;
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }


        [AllowAnonymous]
        [HttpPost("register")]
        [EnableRateLimiting("auth")]
        public async Task<IActionResult> PostRegister(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage, statusCode: StatusCodes.Status400BadRequest);
            }
            if (await _authService.IsEmailAlreadyRegistered(registerDTO.Email))
            {
                return Conflict(new { error = "An account with this email already exists" });
            }
            var (response, error) = await _authService.RegisterUser(registerDTO);

            if (response == null)
            {
                return Problem(error, statusCode: StatusCodes.Status400BadRequest);

            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [EnableRateLimiting("auth")]
        public async Task<IActionResult> PostLogin(LoginDTO loginDTO)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage, statusCode: StatusCodes.Status400BadRequest);
            }
            var response = await _authService.LoginUser(loginDTO);
            if (response == null)
            {
                return Problem("Invalid email or password", statusCode: StatusCodes.Status404NotFound);

            }
            return Ok(response);

        }


        [AllowAnonymous]
        [HttpPost("google-auth")]
        [EnableRateLimiting("auth")]
        public async Task<IActionResult> GoogleAuth([FromBody] GoogleAuthRequest request)
        {
            
            var result  = await _authService.SignInWithGoogle(request);

            if (result.response == null)
                return Problem(result.error ?? "Google authentication failed", statusCode: 400);

            return Ok(result.response);
        }


        [HttpGet("logout")]
        [Authorize]
        [EnableRateLimiting("writes")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync(User.GetUserId());

            return NoContent();
        }


        [AllowAnonymous]
        [HttpPost("generate-new-jwt-token")]
        [EnableRateLimiting("auth")]
        public async Task<IActionResult> GenerateNewAccessToken(TokenModel tokenModel)
        {
            if (tokenModel == null)
                return BadRequest("Invalid client request");
            var response = await _authService.GenerateRefreshToken(tokenModel);

            if (response == null)
                return Problem("JWT Token expired, kindly login Again",
               statusCode: StatusCodes.Status401Unauthorized);
            return Ok(response);

        }

        [HttpPost("change-password")]
        [Authorize]
        [EnableRateLimiting("credentials")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userid = User.GetUserId();
            var result = await _authService.ChangePassword(request, userid);
            if (!result.isSuccess)
                return Problem(statusCode: StatusCodes.Status401Unauthorized, detail: result.error);
            return Ok(new { message = "Password changed successfully." });
        }


    }
}
