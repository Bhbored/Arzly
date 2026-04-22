using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.Notification;
using Arzly.Shared.DTOs.Response.Notification;

namespace Arzly.Api.Mappings
{
    public static class NotificationMapping
    {
        public static NotificationResponse ToResponse(this Notification entity)
        {
            return new NotificationResponse
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ChatterId = entity.ChatterId,
                ListingId = entity.ListingId,
                Title = entity.Title,
                Body = entity.Body,
                Source = entity.Source,
                IsBroadcast = entity.IsBroadcast,
                IsRead = entity.IsRead,
                CreatedAt = entity.CreatedAt,
                ReadAt = entity.ReadAt,
                DeepLink = entity.DeepLink,
                ActionType = entity.ActionType,
                Metadata = entity.Metadata,
                ExpiresAt = entity.ExpiresAt
            };
        }

        public static Notification ToEntity(this NotificationAddRequest request)
        {
            return new Notification
            {
                UserId = request.UserId,
                ChatterId = request.ChatterId,
                ListingId = request.ListingId,
                Title = request.Title,
                Body = request.Body,
                Source = request.Source,
                IsBroadcast = request.IsBroadcast,
                DeepLink = request.DeepLink,
                ActionType = request.ActionType,
                Metadata = request.Metadata,
                ExpiresAt = request.ExpiresAt
            };
        }

        public static Notification ToEntity(this NotificationUpdateRequest request)
        {
            return new Notification
            {
                Id = request.Id,
                IsRead = request.IsRead,
                ReadAt = request.ReadAt
            };
        }

        public static NotificationUpdateRequest ToUpdateRequest(this NotificationResponse response)
        {
            return new NotificationUpdateRequest
            {
                Id = response.Id,
                IsRead = response.IsRead,
                ReadAt = response.ReadAt
            };
        }
    }
}
