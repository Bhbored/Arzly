using Arzly.Api.Application.Services;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Api.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<AppUser, string>, IUserRepository
    {

        private readonly ILogger<UserRepository> _logger;
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext context, ILogger<UserRepository> logger) : base(context)
        {
            _logger = logger;
            _db = context;
        }

        public async Task<AppUser?> GetByFireBaseIdAsync(string id)
        {
            _logger.LogInformation($"Reached {GetType().Name} - GetByFireBaseIdAsync");
            return await _db.Users
                .FirstOrDefaultAsync(x => x.FirebaseUid == id);
        }
    }
}
