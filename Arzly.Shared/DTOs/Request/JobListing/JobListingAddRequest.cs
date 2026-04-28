using Arzly.Shared.Enums.JobListing;
using Arzly.Shared.Enums.Listing;
using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.JobListing
{
    public class JobListingAddRequest
    {
        [Required(ErrorMessage = "Job type is required.")]
        public JobType Type { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(5000, ErrorMessage = "Description cannot exceed 5000 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location ID is required.")]
        public Guid LocationId { get; set; }

        public ContactMethod? ContactMethod { get; set; }

        [Required(ErrorMessage = "Job field is required.")]
        public JobField? JobField { get; set; }

        [Required(ErrorMessage = "Experience level is required.")]
        public ExperienceLevel? ExperienceLevel { get; set; }

        [Required(ErrorMessage = "Education level is required.")]
        public EducationLevel? EducationLevel { get; set; }

        [Required(ErrorMessage = "Employment type is required.")]
        public EmploymentType? EmploymentType { get; set; }

        public WorkplaceType? WorkplaceType { get; set; }

        public double? SalaryMin { get; set; }
        public double? SalaryMax { get; set; }

        public List<JobLanguage>? Languages { get; set; }

        [Required(ErrorMessage = "Owner ID is required.")]
        public string OwnerId { get; set; } = string.Empty;

        public DateTime? ExpiresAt { get; set; }
    }
}
