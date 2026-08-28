using Arzly.Api.Application.Contracts.Users;
using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.UserReport;
using Arzly.Shared.DTOs.Response.UserReport;

namespace Arzly.Api.Application.Services.Users
{
    public class UserReportService : IUserReportService
    {
        private readonly IUserReportRepository _reportRepository;

        public UserReportService(IUserReportRepository repository)
        {
            _reportRepository = repository;
        }

        public async Task<List<UserReportResponse>> GetAllAsync() =>
            (await _reportRepository.GetAllAsync()).Select(entity => entity.ToResponse()).ToList();

        public async Task<UserReportResponse?> CreateAsync(UserReportAddRequest? createDto, Guid userId)
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

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            var report = await _reportRepository.GetByIdAsync(id);
            return report is not null && await _reportRepository.Delete(report);
        }
    }
}
