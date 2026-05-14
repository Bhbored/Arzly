using Arzly.Shared.DTOs.Response.JobListing;

namespace Arzly.Client.Services.Contracts;

public interface IJobListingApiClient
{
    Task<List<JobListingResponse>> GetAllJobListingsAsync();
    Task<JobListingResponse?> GetJobListingByIdAsync(Guid id);
}
