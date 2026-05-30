using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.UserReport
{
    public class UserReportUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }

        public bool IsResolved { get; set; }
        public Guid? ResolvedById { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }
}
