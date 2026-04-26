using Arzly.Api.Infrastructure.Identity;
using Arzly.Shared.Enums.JobListing;
using Arzly.Shared.Enums.Listing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.Entities
{
    public class JobListing
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();


        [Required]
        public JobType Type { get; set; }


        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(5000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public Guid LocationId { get; set; }

        [ForeignKey(nameof(LocationId))]
        public virtual PickupLocation Location { get; set; } = null!;

        public ContactMethod? ContactMethod { get; set; }

        [Required]
        public JobField? JobField { get; set; }

        [Required]
        public ExperienceLevel? ExperienceLevel { get; set; }

        [Required]
        public EducationLevel? EducationLevel { get; set; }

        [Required]
        public EmploymentType? EmploymentType { get; set; }

        public WorkplaceType? WorkplaceType { get; set; }

        public double? SalaryMin { get; set; }
        public double? SalaryMax { get; set; }

        public List<JobLanguage>? Languages { get; set; }


        [Required]
        public string OwnerId { get; set; } = string.Empty;

        [ForeignKey(nameof(OwnerId))]
        public virtual AppUser Owner { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }
        public JobStatus Status { get; set; } = JobStatus.Active;

        // Promotion
        public bool IsPromoted { get; set; } = false;
        public PromotionType? PromotionType { get; set; }
        public DateTime? PromotionStartDate { get; set; }
        public DateTime? PromotionEndDate { get; set; }

        public virtual ICollection<Chat>? RelatedChats { get; set; }
    }
}
