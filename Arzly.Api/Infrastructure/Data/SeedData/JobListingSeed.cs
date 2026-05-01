using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums;
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
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Type = JobType.Seeker,
                Title = " FiveToTenYears Software Engineer",
                Description = "We are looking for an experienced software engineer to join our growing team. Must have 5+ years of experience with .NET and React. Remote-friendly with flexible hours.",
                Name = "John Doe",
                Email = "john@example.com",
                PhoneNumber = "+1-555-0201",
                ContactMethod = ContactMethod.PhoneNumber,
                JobField = JobField.InformationTechnology,
                ExperienceLevel = ExperienceLevel. FiveToTenYears,
                EducationLevel = EducationLevel.Bachelors,
                EmploymentType = EmploymentType.FullTime,
                WorkplaceType = WorkplaceType.Hybrid,
                SalaryMin = 90000,
                SalaryMax = 120000,
                OwnerId = "user-1-id",
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                ExpiresAt = DateTime.UtcNow.AddDays(20),
                Status = JobStatus.Active,
                IsPromoted = true,
                PromotionType = PromotionType.Premium,
                PromotionStartDate = DateTime.UtcNow.AddDays(-5),
                PromotionEndDate = DateTime.UtcNow.AddDays(9),
                BaseLocation = LocationPreset.BintJbeilNabatieh,
                LocationTitle = "BintJbeilNabatieh"

            },
            new JobListing
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Type = JobType.Available,
                Title = "Customer Service Representative",
                Description = "Part-time customer service role for retail store. Evening and weekend shifts available. Great opportunity for students.",
                Name = "Bourhan Hassoun",
                Email = "bhbored2022@gmail.com",
                PhoneNumber = "+1-555-0202",
                ContactMethod = ContactMethod.Both,
                JobField = JobField.CustomerService,
                ExperienceLevel = ExperienceLevel.OneToTwoYears,
                EducationLevel = EducationLevel.HighSchool,
                EmploymentType = EmploymentType.PartTime,
                WorkplaceType = WorkplaceType.OnSite,
                SalaryMin = 18,
                SalaryMax = 22,
                OwnerId = "user-2-id",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                ExpiresAt = DateTime.UtcNow.AddDays(25),
                Status = JobStatus.Active,
                BaseLocation = LocationPreset.TripoliNorthLebanon,
                LocationTitle = "Tripoli"
            },
            new JobListing
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Type = JobType.Available,
                Title = "Marketing Manager",
                Description = "Dynamic marketing manager needed for fast-paced startup. Lead our digital marketing efforts, manage social media, and develop brand strategy.",
                Name = "John Doe",
                Email = "john@example.com",
                PhoneNumber = "+1-555-0203",
                ContactMethod = ContactMethod.InAppChat,
                JobField = JobField.Marketing,
                ExperienceLevel = ExperienceLevel.TwoToFiveYears,
                EducationLevel = EducationLevel.Bachelors,
                EmploymentType = EmploymentType.FullTime,
                WorkplaceType = WorkplaceType.Remote,
                SalaryMin = 70000,
                SalaryMax = 90000,
                OwnerId = "user-1-id",
                CreatedAt = DateTime.UtcNow.AddDays(-15),
                ExpiresAt = DateTime.UtcNow.AddDays(15),
                Status = JobStatus.Active,
                BaseLocation = LocationPreset.BintJbeilNabatieh,
                LocationTitle = "BintJbeilNabatieh"
            },
            new JobListing
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                Type = JobType.Seeker,
                Title = "Freelance Graphic Designer",
                Description = "Seeking talented graphic designer for ongoing project work. Must be proficient in Adobe Creative Suite. Portfolio required.",
                Name = "Bourhan Hassoun",
                Email = "bhbored2022@gmail.com",
                PhoneNumber = "+1-555-0204",
                ContactMethod = ContactMethod.PhoneNumber,
                JobField = JobField.Design,
                ExperienceLevel = ExperienceLevel.OneToTwoYears,
                EducationLevel = EducationLevel.Bachelors,
                EmploymentType = EmploymentType.Contract,
                WorkplaceType = WorkplaceType.Remote,
                SalaryMin = 40,
                SalaryMax = 60,
                OwnerId = "user-2-id",
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                ExpiresAt = DateTime.UtcNow.AddDays(27),
                Status = JobStatus.Active,
                BaseLocation = LocationPreset.HermelBaalbekHermel,
                LocationTitle = "Hermel"
            },
            new JobListing
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Type = JobType.Seeker,
                Title = "Registered Nurse - ICU",
                Description = "ICU nurse position at major hospital. Competitive salary, excellent benefits. Night shift differential included.",
                Name = "John Doe",
                Email = "john@example.com",
                PhoneNumber = "+1-555-0205",
                ContactMethod = ContactMethod.Both,
                JobField = JobField.Healthcare,
                ExperienceLevel = ExperienceLevel. FiveToTenYears,
                EducationLevel = EducationLevel.Bachelors,
                EmploymentType = EmploymentType.FullTime,
                WorkplaceType = WorkplaceType.OnSite,
                SalaryMin = 85000,
                SalaryMax = 105000,
                OwnerId = "user-1-id",
                CreatedAt = DateTime.UtcNow.AddDays(-20),
                ExpiresAt = DateTime.UtcNow.AddDays(10),
                 BaseLocation = LocationPreset.ZahleBeirqaa,
                LocationTitle = "Zahle"
            }
        };
    }
}
