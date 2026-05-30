using Arzly.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.UserReport
{
    public class UserReportAddRequest
    {
        [Required(ErrorMessage = "Reporter ID is required.")]
        public Guid ReporterId { get; set; }

        [Required(ErrorMessage = "Reported user ID is required.")]
        public Guid ReportedUserId { get; set; }

        public Guid? ChatId { get; set; }

        [Required(ErrorMessage = "Report reason is required.")]
        public ReportReasonType Reason { get; set; }

        [MaxLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
        public string? Notes { get; set; }
    }
}
