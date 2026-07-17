using Arzly.Api.Application.Contracts.Communications;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Arzly.Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task JoinChat(Guid chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task LeaveChat(Guid chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task SendMessage(Guid chatId, string text)
        {
            var userId = Guid.Parse(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }
            var message = await _chatService.SendMessageAndGetMessageAsync(chatId, text, userId);
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", message);
        }

        public async Task MarkMessageAsRead(Guid messageId, Guid chatId)
        {
            var userId = Guid.Parse(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }
            await _chatService.MarkMessageAsReadAsync(messageId, userId);
            await Clients.Group(chatId.ToString()).SendAsync("MessageRead", messageId, userId);
        }
    }
}
