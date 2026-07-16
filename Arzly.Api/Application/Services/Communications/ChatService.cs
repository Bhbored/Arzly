using Arzly.Api.Application.Contracts.Communications;
using Arzly.Api.Application.Contracts.Listings;
using Arzly.Api.Application.Contracts.Users;
using Arzly.Api.Domain.Contracts.Communications;
using Arzly.Api.Domain.Entities.Communications;
using Arzly.Api.Mappings;
using Arzly.Shared.Constants;
using Arzly.Shared.DTOs.Request.Chat;
using Arzly.Shared.DTOs.Response.Chat;

namespace Arzly.Api.Application.Services.Communications
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _repository;
        private readonly IListingService _listingService;
        private readonly IUserProfileService _userProfileService;
        private readonly ILogger<ChatService> _logger;

        public ChatService(IChatRepository repository, IListingService listingService, IUserProfileService userProfileService, ILogger<ChatService> logger)
        {
            _repository = repository;
            _listingService = listingService;
            _userProfileService = userProfileService;
            _logger = logger;
        }

        public async Task<List<ChatResponse>> GetUserChatsAsync(Guid userId, bool isArchived,
            bool isDiscontinued, int pageSize, int currentPage)
        {
            _logger.LogInformation("{Service}.GetUserChatsAsync({UserId}) - Before", GetType().Name, userId);

            if (userId == Guid.Empty)
            {
                _logger.LogError("{Service}.GetUserChatsAsync - Empty userId provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            var entities = await _repository.GetUserChatsAsync(userId, isArchived, isDiscontinued, pageSize, currentPage);
            var result = entities.ConvertAll(x => x.ToResponse());

            _logger.LogInformation("{Service}.GetUserChatsAsync({UserId}) - After, count {Count}", GetType().Name, userId, result.Count);
            return result;
        }

        public async Task<ChatResponse> GetByIdWithMessagesAsync(Guid id)
        {
            _logger.LogInformation("{Service}.GetByIdWithMessagesAsync({Id}) - Before", GetType().Name, id);

            if (id == Guid.Empty)
            {
                _logger.LogError("{Service}.GetByIdWithMessagesAsync - Empty id provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            var entity = await _repository.GetByIdWithMessagesAsync(id);
            if (entity is null)
            {
                _logger.LogError("{Service}.GetByIdWithMessagesAsync - No Chat found with id {Id}", GetType().Name, id);
                throw new ArgumentException($"{ExceptionMessages.NoObjectWithId} - {id}");
            }

            _logger.LogInformation("{Service}.GetByIdWithMessagesAsync({Id}) - After", GetType().Name, id);
            return entity.ToResponse();
        }

        public async Task<ChatResponse> GetByIdWithMessagesAsync(Guid id, int pageSize, int currentPage)
        {
            _logger.LogInformation("{Service}.GetByIdWithMessagesAsync({Id}, {PageSize}, {CurrentPage}) - Before", GetType().Name, id, pageSize, currentPage);

            if (id == Guid.Empty)
            {
                _logger.LogError("{Service}.GetByIdWithMessagesAsync - Empty id provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            var entity = await _repository.GetByIdWithMessagesAsync(id, pageSize, currentPage);
            if (entity is null)
            {
                _logger.LogError("{Service}.GetByIdWithMessagesAsync - No Chat found with id {Id}", GetType().Name, id);
                throw new ArgumentException($"{ExceptionMessages.NoObjectWithId} - {id}");
            }

            _logger.LogInformation("{Service}.GetByIdWithMessagesAsync({Id}) - After", GetType().Name, id);
            return entity.ToResponse();
        }

        public async Task<ChatResponse> GetByListingIdWithMessagesAsync(Guid listingId)
        {
            _logger.LogInformation("{Service}.GetByListingIdWithMessagesAsync({ListingId}) - Before", GetType().Name, listingId);

            if (listingId == Guid.Empty)
            {
                _logger.LogError("{Service}.GetByListingIdWithMessagesAsync - Empty listingId provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            var entity = await _repository.GetByListingIdWithMessagesAsync(listingId);
            if (entity is null)
            {
                _logger.LogError("{Service}.GetByListingIdWithMessagesAsync - No Chat found with listingId {ListingId}", GetType().Name, listingId);
                throw new ArgumentException($"{ExceptionMessages.NoObjectWithId} - {listingId}");
            }

            _logger.LogInformation("{Service}.GetByListingIdWithMessagesAsync({ListingId}) - After", GetType().Name, listingId);
            return entity.ToResponse();
        }

        public async Task<ChatResponse> StartNewChatAsync(ChatAddRequest createDto, Guid userId)
        {
            _logger.LogInformation("{Service}.StartNewChatAsync - Before", GetType().Name);

            if (createDto is null)
            {
                _logger.LogError("{Service}.StartNewChatAsync - Empty createDto provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.EmptyAddRequest);
            }

            if (userId == Guid.Empty)
            {
                _logger.LogError("{Service}.StartNewChatAsync - Empty userId provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            var entity = createDto.ToEntity();
            entity.Id = Guid.NewGuid();
            entity.LastActivity = DateTime.UtcNow;

            if (entity.ListingId.HasValue)
                entity.ListingTitle = await _listingService.GetTitleByIdAsync(entity.ListingId.Value);

            var receiverProfile = await _userProfileService.GetByIdAsync(entity.ReceiverId);
            entity.PersonName = receiverProfile?.FullName ?? string.Empty;

            await _repository.CreateAsync(entity);

            _logger.LogInformation("{Service}.StartNewChatAsync - After, created Chat with id {Id}", GetType().Name, entity.Id);
            return entity.ToResponse();
        }

        public async Task<ChatResponse> ToggleArchiveAsync(Guid id, Guid userId)
        {
            _logger.LogInformation("{Service}.ToggleArchiveAsync({Id}) - Before", GetType().Name, id);

            if (id == Guid.Empty)
            {
                _logger.LogError("{Service}.ToggleArchiveAsync - Empty id provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            var entity = await _repository.GetByIdWithMessagesAsync(id);
            if (entity is null)
            {
                _logger.LogError("{Service}.ToggleArchiveAsync - No Chat found with id {Id}", GetType().Name, id);
                throw new ArgumentException($"{ExceptionMessages.NoObjectWithId} - {id}");
            }

            entity.IsArchived = !entity.IsArchived;
            entity.LastActivity = DateTime.UtcNow;
            var updated = await _repository.UpdateAsync(entity);

            _logger.LogInformation("{Service}.ToggleArchiveAsync({Id}) - After, IsArchived {IsArchived}", GetType().Name, id, updated.IsArchived);
            return updated.ToResponse();
        }

        public async Task<ChatResponse> MarkDiscontinuedAsync(Guid id, Guid userId)
        {
            _logger.LogInformation("{Service}.MarkDiscontinuedAsync({Id}) - Before", GetType().Name, id);

            if (id == Guid.Empty)
            {
                _logger.LogError("{Service}.MarkDiscontinuedAsync - Empty id provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            var entity = await _repository.GetByIdWithMessagesAsync(id);
            if (entity is null)
            {
                _logger.LogError("{Service}.MarkDiscontinuedAsync - No Chat found with id {Id}", GetType().Name, id);
                throw new ArgumentException($"{ExceptionMessages.NoObjectWithId} - {id}");
            }

            entity.IsDiscontinued = true;
            entity.LastActivity = DateTime.UtcNow;
            var updated = await _repository.UpdateAsync(entity);

            _logger.LogInformation("{Service}.MarkDiscontinuedAsync({Id}) - After", GetType().Name, id);
            return updated.ToResponse();
        }

        public async Task DeleteAsync(Guid id)
        {
            _logger.LogInformation("{Service}.DeleteAsync({Id}) - Before", GetType().Name, id);

            if (id == Guid.Empty)
            {
                _logger.LogError("{Service}.DeleteAsync - Empty id provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            var deleted = await _repository.SoftDeleteAsync(id);
            if (!deleted)
            {
                _logger.LogError("{Service}.DeleteAsync - No Chat found with id {Id}", GetType().Name, id);
                throw new ArgumentException($"{ExceptionMessages.NoObjectWithId} - {id}");
            }

            _logger.LogInformation("{Service}.DeleteAsync({Id}) - After", GetType().Name, id);
        }

        public async Task<ChatResponse> SendMessageAsync(Guid chatId, string text, Guid userId)
        {
            _logger.LogInformation("{Service}.SendMessageAsync({ChatId}) - Before", GetType().Name, chatId);

            if (chatId == Guid.Empty)
            {
                _logger.LogError("{Service}.SendMessageAsync - Empty chatId provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                _logger.LogError("{Service}.SendMessageAsync - Empty message text provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.EmptyAddRequest);
            }

            var chat = await _repository.GetByIdWithMessagesAsync(chatId);
            if (chat is null)
            {
                _logger.LogError("{Service}.SendMessageAsync - No Chat found with id {ChatId}", GetType().Name, chatId);
                throw new ArgumentException($"{ExceptionMessages.NoObjectWithId} - {chatId}");
            }

            Guid receiverId = chat.InitiatorId == userId ? chat.ReceiverId : chat.InitiatorId;

            var message = new ChatMessage
            {
                Id = Guid.NewGuid(),
                ChatId = chatId,
                SenderId = userId,
                ReceiverId = receiverId,
                Text = text,
                SentAt = DateTime.UtcNow
            };

            await _repository.AddMessageAsync(message);

            chat = await _repository.GetByIdWithMessagesAsync(chatId);

            _logger.LogInformation("{Service}.SendMessageAsync({ChatId}) - After", GetType().Name, chatId);
            return chat!.ToResponse();
        }

        public async Task MarkMessageAsReadAsync(Guid messageId, Guid userId)
        {
            _logger.LogInformation("{Service}.MarkMessageAsReadAsync({MessageId}) - Before", GetType().Name, messageId);

            if (messageId == Guid.Empty)
            {
                _logger.LogError("{Service}.MarkMessageAsReadAsync - Empty messageId provided", GetType().Name);
                throw new ArgumentNullException(ExceptionMessages.MissingId);
            }

            var marked = await _repository.MarkMessageAsReadAsync(messageId, userId);
            if (!marked)
            {
                _logger.LogError("{Service}.MarkMessageAsReadAsync - Message {MessageId} not found or user not the receiver", GetType().Name, messageId);
                throw new ArgumentException($"{ExceptionMessages.NoObjectWithId} - {messageId}");
            }

            _logger.LogInformation("{Service}.MarkMessageAsReadAsync({MessageId}) - After", GetType().Name, messageId);
        }
    }
}
