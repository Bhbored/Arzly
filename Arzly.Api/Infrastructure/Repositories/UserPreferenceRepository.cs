using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories
{
    public class UserPreferenceRepository : IUserPreferenceRepository
    {
        public UserPreferenceRepository(DbContext context)
        {
        }
    }
}
