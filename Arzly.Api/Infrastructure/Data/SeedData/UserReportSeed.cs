using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class UserReportSeed
    {
        public static readonly List<UserReport> Data = new()
        {
            new UserReport
            {
                Id = Guid.NewGuid(),
                ReporterId = "user-1-id",
                ReportedUserId = "user-2-id",
                Reason =  ReportReasonType.Spam,
                Notes = "User posting duplicate listings",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                IsResolved = true,
                ResolvedAt = DateTime.UtcNow.AddDays(-3)
            },
            new UserReport
            {
                Id = Guid.NewGuid(),
                ReporterId = "user-2-id",
                ReportedUserId = "user-1-id",
                Reason =  ReportReasonType.InappropriateContent,
                Notes = "Listing contains misleading information",
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                IsResolved = false
            }
        };
    }
}
