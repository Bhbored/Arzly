using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class UserPreferenceRepository : BaseRepository<UserPreference, string>, IUserPreferenceRepository
    {
        public UserPreferenceRepository(DbContext context) : base(context)
        {
        }
    }
}
