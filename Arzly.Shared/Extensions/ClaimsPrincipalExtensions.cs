using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Arzly.Shared.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            string? id = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(id!);
        }
    }
}
//Guid userId = User.GetUserId();

