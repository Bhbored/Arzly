using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Domain.Contracts.Users
{
    public interface IUserProfileRepository : IBaseRepository<UserProfile, Guid>
    {
    }
}
