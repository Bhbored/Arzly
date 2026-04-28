using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.JobListing;
using Arzly.Shared.DTOs.Response.JobListing;

namespace Arzly.Api.Mappings
{
    public static class JobListingMapping
    {
        public static JobListingResponse ToResponse(this JobListing entity)
        {
            return new JobListingResponse
            {
                Id = entity.Id,
                Type = entity.Type,
                Title = entity.Title,
                Description = entity.Description,
                Name = entity.Name,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                LocationId = entity.LocationId,
                ContactMethod = entity.ContactMethod,
                JobField = entity.JobField,
                ExperienceLevel = entity.ExperienceLevel,
                EducationLevel = entity.EducationLevel,
                EmploymentType = entity.EmploymentType,
                WorkplaceType = entity.WorkplaceType,
                SalaryMin = entity.SalaryMin,
                SalaryMax = entity.SalaryMax,
                Languages = entity.Languages,
                OwnerId = entity.OwnerId,
                CreatedAt = entity.CreatedAt,
                ExpiresAt = entity.ExpiresAt,
                Status = entity.Status,
                IsPromoted = entity.IsPromoted,
                PromotionType = entity.PromotionType,
                PromotionStartDate = entity.PromotionStartDate,
                PromotionEndDate = entity.PromotionEndDate
            };
        }

        public static JobListing ToEntity(this JobListingAddRequest request)
        {
            return new JobListing
            {
                Type = request.Type,
                Title = request.Title,
                Description = request.Description,
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                LocationId = request.LocationId,
                ContactMethod = request.ContactMethod,
                JobField = request.JobField,
                ExperienceLevel = request.ExperienceLevel,
                EducationLevel = request.EducationLevel,
                EmploymentType = request.EmploymentType,
                WorkplaceType = request.WorkplaceType,
                SalaryMin = request.SalaryMin,
                SalaryMax = request.SalaryMax,
                Languages = request.Languages,
                OwnerId = request.OwnerId,
                ExpiresAt = request.ExpiresAt
            };
        }

        public static JobListing ToEntity(this JobListingUpdateRequest request, JobListing entity)
        {
            entity.Type = request.Type;
            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.Name = request.Name;
            entity.Email = request.Email;
            entity.PhoneNumber = request.PhoneNumber;
            entity.LocationId = request.LocationId;
            entity.ContactMethod = request.ContactMethod;
            entity.JobField = request.JobField;
            entity.ExperienceLevel = request.ExperienceLevel;
            entity.EducationLevel = request.EducationLevel;
            entity.EmploymentType = request.EmploymentType;
            entity.WorkplaceType = request.WorkplaceType;
            entity.SalaryMin = request.SalaryMin;
            entity.SalaryMax = request.SalaryMax;
            entity.Languages = request.Languages;
            entity.ExpiresAt = request.ExpiresAt;
            entity.Status = request.Status;
            entity.IsPromoted = request.IsPromoted;
            entity.PromotionType = request.PromotionType;
            entity.PromotionStartDate = request.PromotionStartDate;
            entity.PromotionEndDate = request.PromotionEndDate;

            return entity;
        }
    }
}
