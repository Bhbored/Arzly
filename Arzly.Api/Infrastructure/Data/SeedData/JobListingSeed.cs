using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.JobListing;
using Arzly.Shared.Enums.Listing;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class JobListingSeed
    {
        public static readonly List<JobListing> Data = new()
        {
            new JobListing 
            { 
                Id = Guid.Parse("21b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), 
                OwnerId = "8f3b2a1c-5d4e-4f3a-9b8c-7d6e5f4a3b2c", 
                LocationId = Guid.Parse("92b2a2c4-5d4e-4f3a-9b8c-7d6e5f4a3b2c"),
                Title = "Senior C# Developer", 
                Description = "Join our team to build amazing ASP.NET Core apps", 
                Name = "Tech Corp",
                Email = "hr@techcorp.com",
                PhoneNumber = "5551234",
                Type = JobType.Available,
                JobField = JobField.Insurance,
                ExperienceLevel = ExperienceLevel.OneToTwoYears,
                EducationLevel = EducationLevel.Bachelors,
                EmploymentType = EmploymentType.FullTime,
                SalaryMin = 80000,
                SalaryMax = 120000,
                CreatedAt = DateTime.UtcNow
            }
        };
    }
}
