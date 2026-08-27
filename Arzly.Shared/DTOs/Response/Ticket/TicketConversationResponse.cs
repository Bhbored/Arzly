using Arzly.Shared.DTOs.Response.TicketAttachment;
using Arzly.Shared.DTOs.Response.TicketMessage;

namespace Arzly.Shared.DTOs.Response.Ticket;

public class TicketConversationResponse
{
    public TicketResponse Ticket { get; set; } = new();
    public List<TicketMessageResponse> Messages { get; set; } = [];
    public List<TicketAttachmentResponse> Attachments { get; set; } = [];
}
