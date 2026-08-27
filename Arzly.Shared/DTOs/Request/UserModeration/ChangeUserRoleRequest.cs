using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.UserModeration;

public class ChangeUserRoleRequest
{
    [Required]
    public string Role { get; set; } = string.Empty;
}
