using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.JobListing;
using Arzly.Shared.DTOs.Response.JobListing;

namespace Arzly.Api.Application.Contracts
{
    public interface IJobListingService : IBaseService<JobListing, JobListingResponse, JobListingAddRequest, JobListingUpdateRequest, Guid>
    {
    }
}
