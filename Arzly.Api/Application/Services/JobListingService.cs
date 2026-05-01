using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.JobListing;
using Arzly.Shared.DTOs.Response.JobListing;

namespace Arzly.Api.Application.Services
{
    public class JobListingService : BaseService<JobListing, JobListingResponse, JobListingAddRequest, JobListingUpdateRequest, Guid>, IJobListingService
    {
        public JobListingService(IJobListingRepository repository) : base(repository)
        {
        }

        protected override JobListingResponse MapToDto(JobListing entity) => entity.ToResponse();
        protected override JobListing MapToEntity(JobListingAddRequest createDto) => createDto.ToEntity();
        protected override JobListing MapToEntity(JobListingUpdateRequest updateDto) => updateDto.ToEntity();

    }
}
