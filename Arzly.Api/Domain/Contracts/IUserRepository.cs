using Arzly.Api.Infrastructure.Identity;

namespace Arzly.Api.Domain.Contracts
{
    public interface IUserRepository : IBaseRepository<AppUser, string>
    {
        Task<AppUser?> GetByFireBaseIdAsync(string id);
    }
}
