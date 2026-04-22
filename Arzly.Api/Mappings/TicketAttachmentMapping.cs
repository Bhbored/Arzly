using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.TicketAttachment;
using Arzly.Shared.DTOs.Response.TicketAttachment;

namespace Arzly.Api.Mappings
{
    public static class TicketAttachmentMapping
    {
        public static TicketAttachmentResponse ToResponse(this TicketAttachment entity)
        {
            return new TicketAttachmentResponse
            {
                Id = entity.Id,
                TicketId = entity.TicketId,
                FileUrl = entity.FileUrl,
                FileName = entity.FileName,
                ContentType = entity.ContentType,
                FileSize = entity.FileSize,
                UploadedAt = entity.UploadedAt
            };
        }

        public static TicketAttachment ToEntity(this TicketAttachmentAddRequest request)
        {
            return new TicketAttachment
            {
                TicketId = request.TicketId,
                FileUrl = request.FileUrl,
                FileName = request.FileName,
                ContentType = request.ContentType,
                FileSize = request.FileSize
            };
        }

        public static TicketAttachment ToEntity(this TicketAttachmentUpdateRequest request)
        {
            return new TicketAttachment
            {
                Id = request.Id,
                FileName = request.FileName,
                ContentType = request.ContentType
            };
        }

        public static TicketAttachmentUpdateRequest ToUpdateRequest(this TicketAttachmentResponse response)
        {
            return new TicketAttachmentUpdateRequest
            {
                Id = response.Id,
                FileName = response.FileName,
                ContentType = response.ContentType
            };
        }
    }
}
