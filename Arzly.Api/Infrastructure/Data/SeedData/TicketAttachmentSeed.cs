using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Infrastructure.Data.SeedData
{
    public static class TicketAttachmentSeed
    {
        public static readonly List<TicketAttachment> Data = new()
        {
            new TicketAttachment 
            { 
                Id = Guid.Parse("a1b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), 
                TicketId = Guid.Parse("81b1a2c3-4e5f-4f6a-8b7c-9d0e1f2a3b4c"), 
                FileUrl = "https://example.com/screenshot.jpg", 
                FileName = "screenshot.jpg", 
                ContentType = "image/jpeg", 
                FileSize = 1024, 
                UploadedAt = DateTime.UtcNow
            }
        };
    }
}
