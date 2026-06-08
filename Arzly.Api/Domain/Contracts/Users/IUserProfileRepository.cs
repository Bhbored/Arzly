using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities.Users;

namespace Arzly.Api.Domain.Contracts.Users
{
    public interface IUserProfileRepository : IBaseRepository<UserProfile, Guid>
    {
    }
}
