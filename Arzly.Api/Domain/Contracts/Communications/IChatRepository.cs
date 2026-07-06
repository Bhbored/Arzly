using Arzly.Api.Domain.Entities.Communications;

namespace Arzly.Api.Domain.Contracts.Communications
{
    public interface IChatRepository
    {
        Task<List<Chat>> GetUserChatsAsync(Guid userId, bool isArchived, bool isDiscontinued, int pageSize, int currentPage);
        Task<Chat?> GetByIdWithMessagesAsync(Guid id);
        Task<Chat?> GetByIdWithMessagesAsync(Guid id, int pageSize, int currentPage);
        Task<Chat> CreateAsync(Chat entity);
        Task<Chat> UpdateAsync(Chat entity);
        Task<bool> SoftDeleteAsync(Guid id);
        Task<ChatMessage> AddMessageAsync(ChatMessage message);
        Task<ChatMessage?> GetMessageByIdAsync(Guid messageId);
        Task<bool> MarkMessageAsReadAsync(Guid messageId, Guid userId);
    }
}
