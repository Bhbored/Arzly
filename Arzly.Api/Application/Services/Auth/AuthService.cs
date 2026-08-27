using Arzly.Api.Application.Contracts.Auth;
using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Domain.Entities.Users;
using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Constants;
using Arzly.Shared.DTOs.Request.Auth;
using Arzly.Shared.DTOs.Response.Auth;
using Arzly.Shared.Enums;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using System.Data;
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
        private readonly IConfiguration _configuration;
        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager, IJwtService jwtService, IUserProfileRepository profileRepository,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _profileRepository = profileRepository;
            _configuration = configuration;
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
                if (IsBanActive(user))
                    return null;
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                var authenticationResponse = _jwtService.CreateJwtToken(user, role!);
                user.RefreshToken = authenticationResponse.RefreshToken;

                user.RefreshTokenExpirateDate = authenticationResponse.RefreshTokenExpirateDate;
                await _userManager.UpdateAsync(user);
                return authenticationResponse;
            }

            return null;
        }

        public async Task<(AuthenticationResponse? response, string? error)> SignInWithGoogle(GoogleAuthRequest request)
        {
            var payload = await ValidateGoogleToken(request.IdToken);
            if (payload == null)
                return (null, "Invalid Google token");

            var email = payload.Email;
            var googleId = payload.Subject;
            var name = payload.Name;

            if (string.IsNullOrEmpty(email))
                return (null, "Email not provided by Google");

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = await CreateNewGoogleUser(email, googleId, name);
                if (user == null)
                    return (null, "Failed to create user");
            }
            else if (user.AuthMethod != AuthMethod.Firebase)
            {
                user.FirebaseUid = googleId;
                user.AuthMethod = AuthMethod.Firebase;    
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "user";
            var authResponse = _jwtService.CreateJwtToken(user, role);

            user.RefreshToken = authResponse.RefreshToken;
            user.RefreshTokenExpirateDate = authResponse.RefreshTokenExpirateDate;
            await _userManager.UpdateAsync(user);

            return (authResponse, null);
        }

        private async Task<GoogleJsonWebSignature.Payload?> ValidateGoogleToken(string idToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { $"{_configuration["Authentication:Google:ClientId"]}" }
                };
                return await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            }
            catch
            {
                return null;
            }
        }

        private async Task<ApplicationUser?> CreateNewGoogleUser(string email, string googleId, string? name)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                AuthMethod = AuthMethod.Firebase,
                FirebaseUid = googleId,
                CreatedAt = DateTime.UtcNow,
            };

            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
                return null;

            if (await _roleManager.FindByNameAsync("user") == null)
            {
                ApplicationRole role = new() { Name = "user", NormalizedName = "USER" };
                await _roleManager.CreateAsync(role);
            }
            await _userManager.AddToRoleAsync(user, "user");
            var userProfle = new UserProfile()
            {
                FullName = name,
                Email = email,
                UserId = user.Id,
                UpdateddAt = DateTime.UtcNow,
                PhoneNumber = user.PhoneNumber
            };
            await _profileRepository.AddAsync(userProfle);

            return user;
        }
        public async Task<(AuthenticationResponse? response, string? error)> RegisterUser(RegisterDTO registerDTO)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,

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

                var authenticationResponse = _jwtService.CreateJwtToken(user, userRole!);
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
            ClaimsPrincipal? principal;
            try
            {
                principal = _jwtService.GetPrincipleFromJwtToken(tokenModel.Token);
            }
            catch
            {
                return null;
            }
            if (principal == null)
                return null;

            string? email = principal.FindFirstValue(ClaimTypes.Email);

            ApplicationUser? user = await _userManager.FindByEmailAsync(email!);

            if (user == null || IsBanActive(user) || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpirateDate <= DateTime.UtcNow)
                return null;
            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "user";

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

        public async Task<(bool isSuccess, string? error)> ChangePassword(ChangePasswordRequest request, Guid userId)
        {
            if (userId == Guid.Empty)
                return (false, ExceptionMessages.MissingId);
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return (false, ExceptionMessages.NoObjectWithId);
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            string errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
            if (!result.Succeeded)
                return (false, errorMessage);
            return (true, null);

        }

        public async Task LogoutAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return;

            user.RefreshToken = null;
            user.RefreshTokenExpirateDate = null;
            await _userManager.UpdateAsync(user);
        }

        private static bool IsBanActive(ApplicationUser user) =>
            user.IsBanned && (user.BanExpiresAt is null || user.BanExpiresAt > DateTime.UtcNow);

        
    }
}
