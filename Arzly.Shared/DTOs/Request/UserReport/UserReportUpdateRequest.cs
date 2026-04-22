namespace Arzly.Shared.DTOs.Request.UserReport
{
    public class UserReportUpdateRequest
    {
        public Guid Id { get; set; }
        public bool IsResolved { get; set; }
        public string? ResolvedById { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }
}
