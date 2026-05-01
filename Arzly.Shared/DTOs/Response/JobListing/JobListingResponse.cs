using Arzly.Shared.Enums;
using Arzly.Shared.Enums.JobListing;
using Arzly.Shared.Enums.Listing;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Response.JobListing
{
    public class JobListingResponse
    {
        public Guid Id { get; set; }
        public JobType Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public ContactMethod? ContactMethod { get; set; }
        public JobField? JobField { get; set; }
        public ExperienceLevel? ExperienceLevel { get; set; }
        public EducationLevel? EducationLevel { get; set; }
        public EmploymentType? EmploymentType { get; set; }
        public WorkplaceType? WorkplaceType { get; set; }
        public double? SalaryMin { get; set; }
        public double? SalaryMax { get; set; }
        public List<JobLanguage>? Languages { get; set; }
        public string OwnerId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public JobStatus Status { get; set; }

        public LocationPreset BaseLocation { get; set; }

        public double? lon { get; set; }
        public double? lat { get; set; }

        public string LocationTitle { get; set; } = string.Empty;

        // Promotion
        public bool IsPromoted { get; set; }
        public PromotionType? PromotionType { get; set; }
        public DateTime? PromotionStartDate { get; set; }
        public DateTime? PromotionEndDate { get; set; }
    }
}
