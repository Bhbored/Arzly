using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.TicketMessage;
using Arzly.Shared.DTOs.Response.TicketMessage;

namespace Arzly.Api.Mappings
{
    public static class TicketMessageMapping
    {
        public static TicketMessageResponse ToResponse(this TicketMessage entity)
        {
            return new TicketMessageResponse
            {
                Id = entity.Id,
                TicketId = entity.TicketId,
                SenderId = entity.SenderId,
                ReceiverId = entity.ReceiverId,
                Message = entity.Message,
                SentAt = entity.SentAt,
                IsInternalNote = entity.IsInternalNote
            };
        }

        public static TicketMessage ToEntity(this TicketMessageAddRequest request)
        {
            return new TicketMessage
            {
                TicketId = request.TicketId,
                SenderId = request.SenderId,
                ReceiverId = request.ReceiverId,
                Message = request.Message,
                IsInternalNote = request.IsInternalNote
            };
        }

        public static TicketMessage ToEntity(this TicketMessageUpdateRequest request)
        {
            return new TicketMessage
            {
                Id = request.Id,
                Message = request.Message,
                IsInternalNote = request.IsInternalNote
            };
        }

        public static TicketMessageUpdateRequest ToUpdateRequest(this TicketMessageResponse response)
        {
            return new TicketMessageUpdateRequest
            {
                Id = response.Id,
                Message = response.Message,
                IsInternalNote = response.IsInternalNote
            };
        }
    }
}
