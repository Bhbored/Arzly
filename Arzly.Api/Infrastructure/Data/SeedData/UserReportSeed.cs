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
                Id = Guid.Parse("c1b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), 
                ReporterId = "7c9e6679-7425-40de-944b-e07fc1f90ae7", 
                ReportedUserId = "8f3b2a1c-5d4e-4f3a-9b8c-7d6e5f4a3b2c", 
                Reason = ReportReasonType.Spam, 
                Notes = "User is spamming messages", 
                CreatedAt = DateTime.UtcNow
            }
        };
    }
}
