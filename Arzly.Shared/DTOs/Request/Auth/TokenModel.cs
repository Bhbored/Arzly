using System;
using System.Collections.Generic;
using System.Text;

namespace Arzly.Shared.DTOs.Request.Auth
{
    public class TokenModel
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }

    }
}
