using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.UserReport;
using Arzly.Shared.DTOs.Response.UserReport;

namespace Arzly.Api.Application.Services
{
    public class UserReportService : BaseService<UserReport, UserReportResponse, UserReportAddRequest, UserReportUpdateRequest, Guid>, IUserReportService
    {
        public UserReportService(IUserReportRepository repository) : base(repository)
        {
        }

        protected override UserReportResponse MapToDto(UserReport entity) => entity.ToResponse();
        protected override UserReport MapToEntity(UserReportAddRequest createDto) => createDto.ToEntity();
        protected override UserReport MapToEntity(UserReportUpdateRequest updateDto) => updateDto.ToEntity();
    }
}
