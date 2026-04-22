using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.Ticket;
using Arzly.Shared.DTOs.Response.Ticket;

namespace Arzly.Api.Mappings
{
    public static class TicketMapping
    {
        public static TicketResponse ToResponse(this Ticket entity)
        {
            return new TicketResponse
            {
                Id = entity.Id,
                Subject = entity.Subject,
                Status = entity.Status,
                Priority = entity.Priority,
                CreatedAt = entity.CreatedAt,
                LastUpdatedAt = entity.LastUpdatedAt,
                ClosedAt = entity.ClosedAt,
                UserId = entity.UserId,
                AssignedToId = entity.AssignedToId,
                ListingId = entity.ListingId
            };
        }

        public static Ticket ToEntity(this TicketAddRequest request)
        {
            return new Ticket
            {
                Subject = request.Subject,
                Priority = request.Priority,
                UserId = request.UserId,
                ListingId = request.ListingId
            };
        }

        public static Ticket ToEntity(this TicketUpdateRequest request)
        {
            return new Ticket
            {
                Id = request.Id,
                Subject = request.Subject,
                Status = request.Status,
                Priority = request.Priority,
                AssignedToId = request.AssignedToId,
                ClosedAt = request.ClosedAt
            };
        }

        public static TicketUpdateRequest ToUpdateRequest(this TicketResponse response)
        {
            return new TicketUpdateRequest
            {
                Id = response.Id,
                Subject = response.Subject,
                Status = response.Status,
                Priority = response.Priority,
                AssignedToId = response.AssignedToId,
                ClosedAt = response.ClosedAt
            };
        }
    }
}
