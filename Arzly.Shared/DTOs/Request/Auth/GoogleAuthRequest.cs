using System;
using System.Collections.Generic;
using System.Text;

namespace Arzly.Shared.DTOs.Request.Auth
{
    public class GoogleAuthRequest
    {
        public string IdToken { get; set; } = string.Empty;
    }
}
