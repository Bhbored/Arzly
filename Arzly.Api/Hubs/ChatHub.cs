using Arzly.Api.Application.Contracts.Communications;
using Arzly.Api.Filters.HubFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Arzly.Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(IChatService chatService, ILogger<ChatHub> logger)
        {
            _chatService = chatService;
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("{Hub}.OnConnectedAsync - ConnectionId {ConnectionId}, User {UserId}",
                GetType().Name, Context.ConnectionId, Context.UserIdentifier);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation("{Hub}.OnDisconnectedAsync - ConnectionId {ConnectionId}, User {UserId}, Reason {Reason}",
                GetType().Name, Context.ConnectionId, Context.UserIdentifier, exception?.Message ?? "none");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinChat(Guid chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task LeaveChat(Guid chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        [HubRateLimit]
        public async Task SendMessage(Guid chatId, string text)
        {
            var userId = GetUserId();
            try
            {
                var message = await _chatService.SendMessageAndGetMessageAsync(chatId, text, userId);
                await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", message);
            }
            catch (ArgumentException ex)
            {
                throw new HubException(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new HubException(ex.Message);
            }
        }

        public async Task MarkMessageAsRead(Guid messageId, Guid chatId)
        {
            var userId = GetUserId();
            try
            {
                await _chatService.MarkMessageAsReadAsync(messageId, userId);
                await Clients.Group(chatId.ToString()).SendAsync("MessageRead", messageId, userId);
            }
            catch (ArgumentException ex)
            {
                throw new HubException(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new HubException(ex.Message);
            }
        }

        private Guid GetUserId()
        {
            var id = Context.UserIdentifier;
            if (!Guid.TryParse(id, out var userId) || userId == Guid.Empty)
            {
                throw new HubException("Unauthorized");
            }

            return userId;
        }
    }
}
