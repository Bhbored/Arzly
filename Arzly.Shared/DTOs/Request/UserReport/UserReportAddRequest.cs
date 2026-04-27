using Arzly.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.UserReport
{
    public class UserReportAddRequest
    {
        [Required(ErrorMessage = "Reporter ID is required.")]
        public string ReporterId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Reported user ID is required.")]
        public string ReportedUserId { get; set; } = string.Empty;

        public Guid? ChatId { get; set; }

        [Required(ErrorMessage = "Report reason is required.")]
        public ReportReasonType Reason { get; set; }

        [MaxLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
        public string? Notes { get; set; }
    }
}
