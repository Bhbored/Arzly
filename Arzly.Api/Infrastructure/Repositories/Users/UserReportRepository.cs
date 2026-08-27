using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities.Users;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories.Users
{
    public class UserReportRepository : BaseRepository<UserReport, Guid>, IUserReportRepository
    {
        private readonly AppDbContext _db;
        public UserReportRepository(AppDbContext context) : base(context)
        {
            _db = context;
        }

        public Task<UserReport?> GetByIdForUserAsync(Guid id, Guid userId, bool canModerate) =>
            _db.UserReports.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && (canModerate || x.ReporterId == userId));

        public async Task<UserReport?> ResolveAsync(Guid id, Guid resolverId, bool isResolved)
        {
            var report = await _db.UserReports.FirstOrDefaultAsync(x => x.Id == id);
            if (report is null)
                return null;
            report.IsResolved = isResolved;
            report.ResolvedById = isResolved ? resolverId : null;
            report.ResolvedAt = isResolved ? DateTime.UtcNow : null;
            await _db.SaveChangesAsync();
            return report;
        }
    }
}
