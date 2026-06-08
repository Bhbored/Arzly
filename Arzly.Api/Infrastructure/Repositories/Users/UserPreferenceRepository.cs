using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories.Users
{
    public class UserPreferenceRepository : IUserPreferenceRepository
    {
        public UserPreferenceRepository(DbContext context)
        {
        }
    }
}
