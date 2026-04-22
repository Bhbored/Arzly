using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.UserActivityLog;
using Arzly.Shared.DTOs.Response.UserActivityLog;

namespace Arzly.Api.Mappings
{
    public static class UserActivityLogMapping
    {
        public static UserActivityLogResponse ToResponse(this UserActivityLog entity)
        {
            return new UserActivityLogResponse
            {
                Id = entity.Id,
                ActorId = entity.ActorId,
                ActorRole = entity.ActorRole,
                ActionType = entity.ActionType,
                TargetType = entity.TargetType,
                TargetId = entity.TargetId,
                IPAddress = entity.IPAddress,
                UserAgent = entity.UserAgent,
                Details = entity.Details,
                Timestamp = entity.Timestamp,
                IsSuccess = entity.IsSuccess,
                ErrorMessage = entity.ErrorMessage,
                DurationMs = entity.DurationMs
            };
        }

        public static UserActivityLog ToEntity(this UserActivityLogAddRequest request)
        {
            return new UserActivityLog
            {
                ActorId = request.ActorId,
                ActorRole = request.ActorRole,
                ActionType = request.ActionType,
                TargetType = request.TargetType,
                TargetId = request.TargetId,
                IPAddress = request.IPAddress,
                UserAgent = request.UserAgent,
                Details = request.Details,
                IsSuccess = request.IsSuccess,
                ErrorMessage = request.ErrorMessage,
                DurationMs = request.DurationMs
            };
        }

       
    }
}
