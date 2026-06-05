using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Shared.Constants;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories.Users
{
    public class UserProfileRepository : BaseRepository<UserProfile, Guid>, IUserProfileRepository
    {
        private readonly AppDbContext _db;

        public UserProfileRepository(AppDbContext context) : base(context)
        {
            _db = context;
        }
        public override async Task<UserProfile> Update(UserProfile entity)
        {

            var oldentity = await _db.UserProfiles.FirstOrDefaultAsync(x => x.UserId == entity.UserId);
            if (oldentity == null)
                throw new ArgumentNullException(nameof(entity), ExceptionMessages.EmptyUpdateRequest);
            oldentity.FullName = entity.FullName;
            oldentity.PhoneNumber = entity.PhoneNumber;
            oldentity.PublicName = entity.PublicName;
            oldentity.IsStore = entity.IsStore;
            oldentity.ProfileImageUrl = entity.ProfileImageUrl;
            oldentity.StoreDescription = entity.StoreDescription;
            oldentity.PublicLocation = entity.PublicLocation;
            await _db.SaveChangesAsync();
            return oldentity;

        }
    }
}
