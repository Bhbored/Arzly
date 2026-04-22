using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.ChatMessage;
using Arzly.Shared.DTOs.Response.ChatMessage;

namespace Arzly.Api.Mappings
{
    public static class ChatMessageMapping
    {
        public static ChatMessageResponse ToResponse(this ChatMessage entity)
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

        public static ChatMessage ToEntity(this ChatMessageAddRequest request)
        {
            return new ChatMessage
            {
                ChatId = request.ChatId,
                SenderId = request.SenderId,
                ReceiverId = request.ReceiverId,
                Text = request.Text
            };
        }

        public static ChatMessage ToEntity(this ChatMessageUpdateRequest request)
        {
            return new ChatMessage
            {
                Id = request.Id,
                Text = request.Text,
                IsRead = request.IsRead,
                ReadAt = request.ReadAt
            };
        }

        public static ChatMessageUpdateRequest ToUpdateRequest(this ChatMessageResponse response)
        {
            return new ChatMessageUpdateRequest
            {
                Id = response.Id,
                Text = response.Text,
                IsRead = response.IsRead,
                ReadAt = response.ReadAt
            };
        }
    }
}
