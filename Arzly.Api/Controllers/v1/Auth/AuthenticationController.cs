using Arzly.Api.Application.Contracts.Auth;
using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.DTOs.Request.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers.v1.Auth
{
    [AllowAnonymous]
    public class AuthenticationController : CustomeControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthenticationController(UserManager<ApplicationUser> userManager, IAuthService authService,
            SignInManager<ApplicationUser> signInManager)
        {
            _authService = authService;
            _userManager = userManager;
            _signInManager = signInManager;
        }



        [HttpPost("register")]
        public async Task<IActionResult> PostRegister(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage, statusCode: StatusCodes.Status400BadRequest);
            }
            if (!await _authService.IsEmailAlreadyRegistered(registerDTO.Email))
            {
                return Conflict(new { error = "An account with this email already exists" });
            }
            var (response, error) = await _authService.RegisterUser(registerDTO);

            if (response != null)
            {
                return Ok(response);
            }
            return Problem(error, statusCode: StatusCodes.Status400BadRequest);
        }


        [HttpPost("login")]
        public async Task<IActionResult> PostLogin(LoginDTO loginDTO)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage, statusCode: StatusCodes.Status400BadRequest);
            }
            var response = await _authService.LoginUser(loginDTO);
            if (response != null)
            {
                return Ok(response);
            }
            return Problem("Invalid email or password", statusCode: StatusCodes.Status404NotFound);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }

        [HttpPost("generate-new-jwt-token")]
        public async Task<IActionResult> GenerateNewAccessToken(TokenModel tokenModel)
        {
            if (tokenModel == null)
                return BadRequest("Invalid client request");
            var response = await _authService.GenerateRefreshToken(tokenModel);

            if (response != null)
                return Ok(response);
            return Problem("JWT Token expired, kindly login Again",
                statusCode: StatusCodes.Status401Unauthorized);
        }


       

    }
}
