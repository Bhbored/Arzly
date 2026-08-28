using Arzly.Api.Application.Contracts.Support;
using Arzly.Api.Domain.Contracts.Support;
using Arzly.Api.Domain.Entities.Support;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.Ticket;
using Arzly.Shared.DTOs.Response.Ticket;
using Arzly.Shared.DTOs.Request.TicketAttachment;
using Arzly.Shared.DTOs.Request.TicketMessage;
using Arzly.Shared.DTOs.Response.TicketAttachment;
using Arzly.Shared.DTOs.Response.TicketMessage;
using Arzly.Shared.Enums.Ticket;

namespace Arzly.Api.Application.Services.Support
{
    public class TicketService : ITicketService
    {
        private const long MaximumAttachmentSize = 10 * 1024 * 1024;
        private static readonly HashSet<string> AllowedAttachmentTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            "image/jpeg", "image/png", "image/webp", "application/pdf"
        };
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository repository)
        {
            _ticketRepository = repository;
        }

        public async Task<TicketResponse?> CreateAsync(TicketAddRequest? createDto, Guid userId)
        {
            if (createDto is null || userId == Guid.Empty)
                throw new ArgumentException("A valid ticket request and user are required");
            var ticket = createDto.ToEntity();
            ticket.UserId = userId;
            ticket.Status = TicketStatus.Open;
            ticket.AssignedToId = null;
            await _ticketRepository.AddAsync(ticket);
            return ticket.ToResponse();
        }

        public async Task<List<TicketResponse>> GetUserTicketsAsync(Guid userId, int pageSize, int currentPage) =>
            (await _ticketRepository.GetUserTicketsAsync(userId, ClampPageSize(pageSize), Math.Max(0, currentPage)))
            .Select(x => x.ToResponse()).ToList();

        public async Task<List<TicketResponse>> GetQueueAsync(int pageSize, int currentPage) =>
            (await _ticketRepository.GetQueueAsync(ClampPageSize(pageSize), Math.Max(0, currentPage)))
            .Select(x => x.ToResponse()).ToList();

        public async Task<TicketConversationResponse> GetConversationAsync(Guid id, Guid userId, bool isStaff)
        {
            var ticket = await GetAccessibleTicket(id, userId, isStaff);
            return new TicketConversationResponse
            {
                Ticket = ticket.ToResponse(),
                Messages = (ticket.Messages ?? []).Where(x => isStaff || !x.IsInternalNote)
                    .Select(x => x.ToResponse()).ToList(),
                Attachments = (ticket.Attachments ?? []).Select(x => x.ToResponse()).ToList()
            };
        }

        public async Task<TicketResponse> SetStatusAsync(Guid id, Guid actorId, TicketStatus status)
        {
            var ticket = await _ticketRepository.UpdateStatusAsync(id, status, actorId)
                ?? throw new ArgumentException("Ticket not found");
            return ticket.ToResponse();
        }

        public async Task<TicketMessageResponse> AddMessageAsync(
            TicketMessageAddRequest request, Guid userId, bool isStaff)
        {
            var ticket = await GetAccessibleTicket(request.TicketId, userId, isStaff);
            if (ticket.Status == TicketStatus.Closed)
                throw new ArgumentException("Closed tickets cannot receive messages");
            if (isStaff && request.ReceiverId != ticket.UserId)
                throw new ArgumentException("Staff messages must target the ticket owner");
            if (!isStaff && (ticket.AssignedToId is null || request.ReceiverId != ticket.AssignedToId))
                throw new ArgumentException("The ticket must be assigned before the user can reply");

            var message = request.ToEntity();
            message.SenderId = userId;
            message.ReceiverId = isStaff ? ticket.UserId : ticket.AssignedToId!.Value;
            message.IsInternalNote = isStaff && request.IsInternalNote;
            return (await _ticketRepository.AddMessageAsync(message)).ToResponse();
        }

        public async Task<TicketAttachmentResponse> AddAttachmentAsync(
            TicketAttachmentAddRequest request, Guid userId, bool isStaff)
        {
            await GetAccessibleTicket(request.TicketId, userId, isStaff);
            if (request.FileSize is <= 0 or > MaximumAttachmentSize)
                throw new ArgumentException("Attachment size must be between 1 byte and 10 MB");
            if (string.IsNullOrWhiteSpace(request.ContentType) || !AllowedAttachmentTypes.Contains(request.ContentType))
                throw new ArgumentException("Only JPEG, PNG, WebP, and PDF attachments are allowed");

            var attachment = request.ToEntity();
            attachment.UploaderId = userId;
            return (await _ticketRepository.AddAttachmentAsync(attachment)).ToResponse();
        }

        private async Task<Ticket> GetAccessibleTicket(Guid id, Guid userId, bool isStaff)
        {
            var ticket = await _ticketRepository.GetConversationAsync(id)
                ?? throw new ArgumentException("Ticket not found");
            if (!isStaff && ticket.UserId != userId)
                throw new UnauthorizedAccessException("The ticket is not accessible to this user");
            return ticket;
        }

        private static int ClampPageSize(int pageSize) => Math.Clamp(pageSize, 1, 100);
    }
}
