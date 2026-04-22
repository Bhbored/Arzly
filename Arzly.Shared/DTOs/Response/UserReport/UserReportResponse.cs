using Arzly.Shared.Enums;

namespace Arzly.Shared.DTOs.Response.UserReport
{
    public class UserReportResponse
    {
        public Guid Id { get; set; }
        public string ReporterId { get; set; } = string.Empty;
        public string ReportedUserId { get; set; } = string.Empty;
        public Guid? ChatId { get; set; }
        public ReportReasonType Reason { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsResolved { get; set; }
        public string? ResolvedById { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }
}
