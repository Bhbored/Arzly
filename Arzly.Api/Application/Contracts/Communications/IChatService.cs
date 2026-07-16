using Arzly.Shared.DTOs.Request.Chat;
using Arzly.Shared.DTOs.Response.Chat;

namespace Arzly.Api.Application.Contracts.Communications
{
    public interface IChatService
    {
        Task<List<ChatResponse>> GetUserChatsAsync(Guid userId, bool isArchived, bool isDiscontinued, int pageSize, int currentPage);
        Task<ChatResponse> GetByIdWithMessagesAsync(Guid id);
        Task<ChatResponse> GetByIdWithMessagesAsync(Guid id, int pageSize, int currentPage);
        Task<ChatResponse?> GetByListingIdWithMessagesAsync(Guid listingId);
        Task<ChatResponse> StartNewChatAsync(ChatAddRequest createDto, Guid userId);
        Task<ChatResponse> ToggleArchiveAsync(Guid id, Guid userId);
        Task<ChatResponse> MarkDiscontinuedAsync(Guid id, Guid userId);
        Task DeleteAsync(Guid id);
        Task<ChatResponse> SendMessageAsync(Guid chatId, string text, Guid userId);
        Task MarkMessageAsReadAsync(Guid messageId, Guid userId);
    }
}
