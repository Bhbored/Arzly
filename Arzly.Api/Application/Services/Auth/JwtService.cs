using Arzly.Api.Application.Contracts.Auth;
using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.DTOs.Response.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Arzly.Api.Application.Services.Auth
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthenticationResponse CreateJwtToken(ApplicationUser user,string role)
        {
            DateTime expiration = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

            Claim[] claims = new Claim[] {
     new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
     new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString()),
     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
     new Claim(ClaimTypes.Email, user.Email),
     new Claim(ClaimTypes.Role, role)
     };

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: expiration,
            signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler tokenHandler = new();
            string token = tokenHandler.WriteToken(tokenGenerator);

            return new AuthenticationResponse()
            {
                UserId = user.Id,
                Token = token,
                Email = user.Email,
                FirebaseId = user.FirebaseUid,
                Expiration = expiration,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpirateDate = DateTime.Now.AddDays(Convert.ToInt32(_configuration["RefreshToken:EXPIRATION_DAYS"]))
            };
        }

        public string GenerateRefreshToken()
        {
            byte[] bytes = new byte[64];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);

        }

        public ClaimsPrincipal? GetPrincipleFromJwtToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = _configuration["jwt:Audience"],
                ValidateIssuer = true,
                ValidIssuer = _configuration["jwt:Issuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:Key"])),
                ValidateLifetime = false
            };
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

            ClaimsPrincipal claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token,
                tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }
            return claimsPrincipal;

        }
    }
}
