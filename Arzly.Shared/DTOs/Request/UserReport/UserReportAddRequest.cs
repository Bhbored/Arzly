using Arzly.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.UserReport
{
    public class UserReportAddRequest
    {
        [Required]
        public string ReporterId { get; set; } = string.Empty;

        [Required]
        public string ReportedUserId { get; set; } = string.Empty;

        public Guid? ChatId { get; set; }

        [Required]
        public ReportReasonType Reason { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }
    }
}
