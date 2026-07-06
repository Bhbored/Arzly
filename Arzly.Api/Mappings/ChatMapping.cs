using Arzly.Api.Domain.Entities.Communications;
using Arzly.Shared.DTOs.Request.Chat;
using Arzly.Shared.DTOs.Response.Chat;
using Arzly.Shared.DTOs.Response.ChatMessage;

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
                ListingId = entity.ListingId,
                JobListingId = entity.JobListingId,
                Messages = entity.Messages?
                    .OrderBy(m => m.SentAt)
                    .Select(m => m.ToMessageResponse())
                    .ToList()
            };
        }

        public static ChatMessageResponse ToMessageResponse(this ChatMessage entity)
        {
            return new ChatMessageResponse
            {
                Id = entity.Id,
                ChatId = entity.ChatId,
                SenderId = entity.SenderId,
                ReceiverId = entity.ReceiverId,
                Text = entity.Text,
                SentAt = entity.SentAt,
                IsRead = entity.IsRead,
                ReadAt = entity.ReadAt
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
    }
}
