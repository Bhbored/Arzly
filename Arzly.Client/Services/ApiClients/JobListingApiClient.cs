using Arzly.Client.Services.Contracts;
using Arzly.Shared.DTOs.Response.JobListing;

namespace Arzly.Client.Services.ApiClients;

public class JobListingApiClient : BaseApiClient, IJobListingApiClient
{
    public JobListingApiClient(HttpClient httpClient, ILogger<JobListingApiClient> logger) 
        : base(httpClient, logger)
    {
    }

    public async Task<List<JobListingResponse>> GetAllJobListingsAsync()
    {
        var result = await GetAsync<List<JobListingResponse>>("arzly/JobListing");
        return result ?? new List<JobListingResponse>();
    }

    public async Task<JobListingResponse?> GetJobListingByIdAsync(Guid id)
    {
        return await GetAsync<JobListingResponse>($"arzly/JobListing/{id}");
    }
}
