using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.Chat;
using Arzly.Shared.DTOs.Response.Chat;

namespace Arzly.Api.Mappings
{
    public static class ChatMapping
    {
        public static ChatResponse ToResponse(this Chat entity)
        {
            return new ChatResponse
            {
                Id = entity.Id,
                ContextRole = entity.ContextRole,
                IsArchived = entity.IsArchived,
                IsDeleted = entity.IsDeleted,
                IsDiscontinued = entity.IsDiscontinued,
                LastActivity = entity.LastActivity,
                InitiatorId = entity.InitiatorId,
                ReceiverId = entity.ReceiverId,
                ListingId = entity.ListingId
            };
        }

        public static Chat ToEntity(this ChatAddRequest request)
        {
            return new Chat
            {
                ContextRole = request.ContextRole,
                InitiatorId = request.InitiatorId,
                ReceiverId = request.ReceiverId,
                ListingId = request.ListingId
            };
        }

        public static Chat ToEntity(this ChatUpdateRequest request)
        {
            return new Chat
            {
                Id = request.Id,
                IsArchived = request.IsArchived,
                IsDeleted = request.IsDeleted,
                IsDiscontinued = request.IsDiscontinued,
                LastActivity = request.LastActivity
            };
        }

        public static ChatUpdateRequest ToUpdateRequest(this ChatResponse response)
        {
            return new ChatUpdateRequest
            {
                Id = response.Id,
                IsArchived = response.IsArchived,
                IsDeleted = response.IsDeleted,
                IsDiscontinued = response.IsDiscontinued,
                LastActivity = response.LastActivity
            };
        }
    }
}
