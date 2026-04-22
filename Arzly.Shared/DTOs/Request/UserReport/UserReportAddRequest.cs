using Arzly.Shared.Enums;

namespace Arzly.Shared.DTOs.Request.UserReport
{
    public class UserReportAddRequest
    {
        public string ReporterId { get; set; } = string.Empty;
        public string ReportedUserId { get; set; } = string.Empty;
        public Guid? ChatId { get; set; }
        public ReportReasonType Reason { get; set; }
        public string? Notes { get; set; }
    }
}
