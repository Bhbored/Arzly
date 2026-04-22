using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.UserReport;
using Arzly.Shared.DTOs.Response.UserReport;

namespace Arzly.Api.Mappings
{
    public static class UserReportMapping
    {
        public static UserReportResponse ToResponse(this UserReport entity)
        {
            return new UserReportResponse
            {
                Id = entity.Id,
                ReporterId = entity.ReporterId,
                ReportedUserId = entity.ReportedUserId,
                ChatId = entity.ChatId,
                Reason = entity.Reason,
                Notes = entity.Notes,
                CreatedAt = entity.CreatedAt,
                IsResolved = entity.IsResolved,
                ResolvedById = entity.ResolvedById,
                ResolvedAt = entity.ResolvedAt
            };
        }

        public static UserReport ToEntity(this UserReportAddRequest request)
        {
            return new UserReport
            {
                ReporterId = request.ReporterId,
                ReportedUserId = request.ReportedUserId,
                ChatId = request.ChatId,
                Reason = request.Reason,
                Notes = request.Notes
            };
        }

        public static UserReport ToEntity(this UserReportUpdateRequest request)
        {
            return new UserReport
            {
                Id = request.Id,
                IsResolved = request.IsResolved,
                ResolvedById = request.ResolvedById,
                ResolvedAt = request.ResolvedAt
            };
        }

        public static UserReportUpdateRequest ToUpdateRequest(this UserReportResponse response)
        {
            return new UserReportUpdateRequest
            {
                Id = response.Id,
                IsResolved = response.IsResolved,
                ResolvedById = response.ResolvedById,
                ResolvedAt = response.ResolvedAt
            };
        }
    }
}
