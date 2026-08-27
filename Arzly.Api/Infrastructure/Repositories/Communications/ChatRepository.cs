using Arzly.Api.Domain.Contracts.Communications;
using Arzly.Api.Domain.Entities.Communications;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories.Communications
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ChatRepository> _logger;

        public ChatRepository(AppDbContext db, ILogger<ChatRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<List<Chat>> GetUserChatsAsync(Guid userId, bool isArchived, bool isDiscontinued, int pageSize, int currentPage)
        {
            _logger.LogInformation("{Repo}.GetUserChatsAsync({UserId})", GetType().Name, userId);

            return await VisibleChats()
                .AsNoTracking()
                .Where(c => (c.InitiatorId == userId || c.ReceiverId == userId)
                    && c.IsArchived == isArchived
                    && c.IsDiscontinued == isDiscontinued)
                .OrderByDescending(c => c.LastActivity)
                .Skip(currentPage * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Chat?> GetByIdWithMessagesAsync(Guid id)
        {
            _logger.LogInformation("{Repo}.GetByIdWithMessagesAsync({Id})", GetType().Name, id);

            return await VisibleChats()
                .Include(c => c.Messages!
                    .OrderBy(m => m.SentAt))
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Chat?> GetByIdWithMessagesAsync(Guid id, int pageSize, int currentPage)
        {
            _logger.LogInformation("{Repo}.GetByIdWithMessagesAsync({Id}, pageSize: {PageSize}, currentPage: {CurrentPage})", GetType().Name, id, pageSize, currentPage);

            return await VisibleChats()
                .Include(c => c.Messages!
                    .OrderByDescending(m => m.SentAt)
                    .Skip(currentPage * pageSize)
                    .Take(pageSize))
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Chat?> GetByListingIdWithMessagesAsync(Guid listingId, Guid userId)
        {
            _logger.LogInformation("{Repo}.GetByListingIdWithMessagesAsync({ListingId})", GetType().Name, listingId);

            return await VisibleChats()
                .Include(c => c.Messages!
                    .OrderBy(m => m.SentAt))
                .FirstOrDefaultAsync(c => c.ListingId == listingId &&
                    (c.InitiatorId == userId || c.ReceiverId == userId));
        }

        public async Task<Chat> CreateAsync(Chat entity)
        {
            _logger.LogInformation("{Repo}.CreateAsync", GetType().Name);

            _db.Chats.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        private IQueryable<Chat> VisibleChats() => _db.Chats.Where(c =>
            (c.ListingId == null || _db.Listings.Any(l => l.Id == c.ListingId)) &&
            (c.JobListingId == null || _db.JobListings.Any(j => j.Id == c.JobListingId)));

        public async Task<Chat> UpdateAsync(Chat entity)
        {
            _logger.LogInformation("{Repo}.UpdateAsync({Id})", GetType().Name, entity.Id);

            var existing = await _db.Chats.FirstOrDefaultAsync(c => c.Id == entity.Id);
            if (existing is null)
                throw new KeyNotFoundException($"Chat with id {entity.Id} not found");

            existing.IsArchived = entity.IsArchived;
            existing.IsDeleted = entity.IsDeleted;
            existing.IsDiscontinued = entity.IsDiscontinued;
            existing.LastActivity = entity.LastActivity;
            existing.DeletedAt = entity.DeletedAt;

            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> GetIsArchivedAsync(Guid id)
        {
            _logger.LogInformation("{Repo}.GetIsArchivedAsync({Id})", GetType().Name, id);

            return await _db.Chats
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => c.IsArchived)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> GetIsDiscontinuedAsync(Guid id)
        {
            _logger.LogInformation("{Repo}.GetIsDiscontinuedAsync({Id})", GetType().Name, id);

            return await _db.Chats
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => c.IsDiscontinued)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SoftDeleteAsync(Guid id)
        {
            _logger.LogInformation("{Repo}.SoftDeleteAsync({Id})", GetType().Name, id);

            var entity = await _db.Chats.FirstOrDefaultAsync(c => c.Id == id);
            if (entity is null) return false;

            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ChatMessage> AddMessageAsync(ChatMessage message)
        {
            _logger.LogInformation("{Repo}.AddMessageAsync - ChatId {ChatId}", GetType().Name, message.ChatId);

            _db.ChatMessages.Add(message);

            var chat = await _db.Chats.FirstOrDefaultAsync(c => c.Id == message.ChatId);
            if (chat is not null)
            {
                chat.LastActivity = DateTime.UtcNow;
            }

            await _db.SaveChangesAsync();
            return message;
        }

        public async Task<ChatMessage?> GetMessageByIdAsync(Guid messageId)
        {
            _logger.LogInformation("{Repo}.GetMessageByIdAsync({Id})", GetType().Name, messageId);

            return await _db.ChatMessages
                .FirstOrDefaultAsync(m => m.Id == messageId);
        }

        public async Task<bool> MarkMessageAsReadAsync(Guid messageId, Guid userId)
        {
            _logger.LogInformation("{Repo}.MarkMessageAsReadAsync({Id})", GetType().Name, messageId);

            var message = await _db.ChatMessages.FirstOrDefaultAsync(m => m.Id == messageId);
            if (message is null) return false;

            if (message.ReceiverId != userId) return false;

            message.IsRead = true;
            message.ReadAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
