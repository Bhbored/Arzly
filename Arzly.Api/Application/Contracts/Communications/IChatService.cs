using Arzly.Shared.DTOs.Request.Chat;
using Arzly.Shared.DTOs.Response.Chat;
using Arzly.Shared.DTOs.Response.ChatMessage;

namespace Arzly.Api.Application.Contracts.Communications
{
    public interface IChatService
    {
        Task<List<ChatResponse>> GetUserChatsAsync(Guid userId, bool isArchived, bool isDiscontinued, int pageSize, int currentPage);
        Task<ChatResponse> GetByIdWithMessagesAsync(Guid id, Guid userId);
        Task<ChatResponse> GetByIdWithMessagesAsync(Guid id, Guid userId, int pageSize, int currentPage);
        Task<ChatResponse?> GetByListingIdWithMessagesAsync(Guid listingId, Guid userId);
        Task<ChatResponse> StartNewChatAsync(ChatAddRequest createDto, Guid userId);
        Task<ChatResponse> ToggleArchiveAsync(Guid id, Guid userId);
        Task<ChatResponse> MarkDiscontinuedAsync(Guid id, Guid userId);
        Task DeleteAsync(Guid id, Guid userId);
        Task<ChatResponse> SendMessageAsync(Guid chatId, string text, Guid userId);
        Task<ChatMessageResponse> SendMessageAndGetMessageAsync(Guid chatId, string text, Guid userId);
        Task MarkMessageAsReadAsync(Guid messageId, Guid userId);
    }
}
