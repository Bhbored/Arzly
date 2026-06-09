using Arzly.Shared.DTOs.Request.Auth;
using Arzly.Shared.DTOs.Response.Auth;
using Google.Apis.Auth;

namespace Arzly.Api.Application.Contracts.Auth
{
    public interface IAuthService
    {
        Task<(AuthenticationResponse? response, string? error)> RegisterUser(RegisterDTO registerDTO);
        Task<AuthenticationResponse?> LoginUser(LoginDTO loginDTO);
        Task<AuthenticationResponse?> GenerateRefreshToken(TokenModel tokenModel);
        Task<bool> IsEmailAlreadyRegistered(string email);
        Task<(bool isSuccess, string? error)> ChangePassword(ChangePasswordRequest request,Guid userId);
        Task<(AuthenticationResponse? response, string? error)> SignInWithGoogle(GoogleAuthRequest request);


    }
}
