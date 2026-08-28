using Arzly.Api.Domain.Entities.Listings;
using Arzly.Shared.DTOs.Request.JobListing;
using Arzly.Shared.DTOs.Response.JobListing;

namespace Arzly.Api.Application.Contracts.Listings
{
    public interface IJobListingService
    {
        Task<List<JobListingResponse>> GetAllAsync();
        Task<JobListingResponse?> GetByIdAsync(Guid id);
        Task<JobListingResponse?> CreateAsync(JobListingAddRequest? request, Guid userId);
        Task<JobListingResponse?> UpdateAsync(JobListingUpdateRequest? request, Guid userId);
        Task<bool> DeleteAsync(Guid id);
    }
}
