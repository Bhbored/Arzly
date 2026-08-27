using Arzly.Api.Application.Contracts.Users;
using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities.Users;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.UserReport;
using Arzly.Shared.DTOs.Response.UserReport;

namespace Arzly.Api.Application.Services.Users
{
    public class UserReportService : BaseService<UserReport, UserReportResponse, UserReportAddRequest, UserReportUpdateRequest, Guid>, IUserReportService
    {
        private readonly IUserReportRepository _reportRepository;

        public UserReportService(IUserReportRepository repository) : base(repository)
        {
            _reportRepository = repository;
        }

        protected override UserReportResponse MapToDto(UserReport entity) => entity.ToResponse();
        protected override UserReport MapToEntity(UserReportAddRequest createDto) => createDto.ToEntity();
        protected override UserReport MapToEntity(UserReportUpdateRequest updateDto) => updateDto.ToEntity();

        public override async Task<UserReportResponse?> CreateAsync(UserReportAddRequest? createDto, Guid userId)
        {
            if (createDto is null || userId == Guid.Empty)
                throw new ArgumentException("A valid report request and reporter are required");
            if (createDto.ReportedUserId == userId)
                throw new ArgumentException("Users cannot report themselves");

            var entity = createDto.ToEntity();
            entity.ReporterId = userId;
            entity.IsResolved = false;
            entity.ResolvedById = null;
            entity.ResolvedAt = null;
            await _reportRepository.AddAsync(entity);
            return entity.ToResponse();
        }

        public async Task<UserReportResponse> GetByIdAsync(Guid id, Guid userId, bool canModerate)
        {
            var report = await _reportRepository.GetByIdForUserAsync(id, userId, canModerate)
                ?? throw new UnauthorizedAccessException("The report is not accessible to this user");
            return report.ToResponse();
        }

        public async Task<UserReportResponse> ResolveAsync(Guid id, Guid resolverId, bool isResolved)
        {
            var report = await _reportRepository.ResolveAsync(id, resolverId, isResolved)
                ?? throw new ArgumentException("Report not found");
            return report.ToResponse();
        }
    }
}
