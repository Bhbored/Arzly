using Arzly.Api.Application.Contracts.Auth;
using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.DTOs.Request.Auth;
using Arzly.Shared.DTOs.Response.Auth;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Arzly.Api.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtService _jwtService;
        private readonly IUserProfileRepository _profileRepository;
        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager, IJwtService jwtService, IUserProfileRepository profileRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _profileRepository = profileRepository;
        }

        public async Task<AuthenticationResponse?> LoginUser(LoginDTO loginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);

                if (user == null)
                {
                    throw new ArgumentNullException("No User Found with this Email");
                }
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                var authenticationResponse = _jwtService.CreateJwtToken(user, role);
                user.RefreshToken = authenticationResponse.RefreshToken;

                user.RefreshTokenExpirateDate = authenticationResponse.RefreshTokenExpirateDate;
                await _userManager.UpdateAsync(user);
                return authenticationResponse;
            }

            return null;
        }

        public async Task<(AuthenticationResponse? response, string? error)> RegisterUser(RegisterDTO registerDTO)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber

            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                if (await _roleManager.FindByNameAsync("user") is null)
                {
                    ApplicationRole role = new()
                    {
                        Name = "user",
                        NormalizedName = "USER"
                    };
                    await _roleManager.CreateAsync(role);
                }
                await _userManager.AddToRoleAsync(user, "user");
                await _signInManager.SignInAsync(user, isPersistent: false);

                var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                var authenticationResponse = _jwtService.CreateJwtToken(user, userRole);
                user.RefreshToken = authenticationResponse.RefreshToken;

                user.RefreshTokenExpirateDate = authenticationResponse.RefreshTokenExpirateDate;
                await _userManager.UpdateAsync(user);


                var userProfle = new UserProfile()
                {
                    FullName = registerDTO.FullName,
                    Email = registerDTO.Email,
                    UserId = user.Id,
                    UpdateddAt = DateTime.UtcNow,
                    PhoneNumber = user.PhoneNumber
                };
                await _profileRepository.AddAsync(userProfle);
                return (authenticationResponse, null);
            }
            else
            {
                string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
                return (null, errorMessage);
            }
        }
        public async Task<AuthenticationResponse?> GenerateRefreshToken(TokenModel tokenModel)
        {
            ClaimsPrincipal? principal = _jwtService.GetPrincipleFromJwtToken(tokenModel.Token);
            if (principal == null)
            {
                throw new ArgumentException("Invalid jwt access token");
            }

            string? email = principal.FindFirstValue(ClaimTypes.Email);

            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpirateDate <= DateTime.Now)
            {
                throw new ArgumentException("Invalid refresh token");
            }
            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            AuthenticationResponse authenticationResponse = _jwtService.CreateJwtToken(user, userRole);

            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpirateDate = authenticationResponse.RefreshTokenExpirateDate;

            await _userManager.UpdateAsync(user);

            return authenticationResponse;
        }


        public async Task<bool> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }
    }
}
