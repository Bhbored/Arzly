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
                PromotionEndDate = entity.PromotionEndDate,
                BaseLocation = entity.BaseLocation,
                lon = entity.lon,
                lat = entity.lat,
                LocationTitle = entity.LocationTitle,
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
                ExpiresAt = request.ExpiresAt,
                BaseLocation = request.BaseLocation,
                lon = request.lon,
                lat = request.lat,
                LocationTitle = request.LocationTitle
            };
        }

        public static JobListing ToEntity(this JobListingUpdateRequest request)
        {
            return new JobListing
            {
                Type = request.Type,
                Title = request.Title,
                Description = request.Description,
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                ContactMethod = request.ContactMethod,
                JobField = request.JobField,
                ExperienceLevel = request.ExperienceLevel,
                EducationLevel = request.EducationLevel,
                EmploymentType = request.EmploymentType,
                WorkplaceType = request.WorkplaceType,
                SalaryMin = request.SalaryMin,
                SalaryMax = request.SalaryMax,
                Languages = request.Languages,
                ExpiresAt = request.ExpiresAt,
                Status = request.Status,
                IsPromoted = request.IsPromoted,
                PromotionType = request.PromotionType,
                PromotionStartDate = request.PromotionStartDate,
                PromotionEndDate = request.PromotionEndDate,
                BaseLocation = request.BaseLocation,
                lon = request.lon,
                lat = request.lat,
                LocationTitle = request.LocationTitle
            };
        }
    }
}
