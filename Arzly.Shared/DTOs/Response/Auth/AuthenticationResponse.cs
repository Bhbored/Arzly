using System;
using System.Collections.Generic;
using System.Text;

namespace Arzly.Shared.DTOs.Response.Auth
{
    public class AuthenticationResponse
    {
        public Guid UserId { get; set; }
        public string? PublicName { get; set; }
        public string? Email { get; set; }
        public string? PublicLocation { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime? RefreshTokenExpirateDate { get; set; }

    }
}
