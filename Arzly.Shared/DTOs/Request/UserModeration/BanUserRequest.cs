using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.UserModeration;

public class BanUserRequest
{
    [Required, MaxLength(500)]
    public string Reason { get; set; } = string.Empty;
    public DateTime? ExpiresAt { get; set; }
}
