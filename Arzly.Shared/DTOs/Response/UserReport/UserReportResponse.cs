using Arzly.Shared.Enums;

namespace Arzly.Shared.DTOs.Response.UserReport
{
    public class UserReportResponse
    {
        public Guid Id { get; set; }
        public Guid ReporterId { get; set; }
        public Guid ReportedUserId { get; set; }
        public Guid? ChatId { get; set; }
        public ReportReasonType Reason { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsResolved { get; set; }
        public Guid? ResolvedById { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }
}
