using Arzly.Api.Domain.Entities;

namespace Arzly.Api.Domain.Contracts
{
    public interface IPickupLocationRepository : IBaseRepository<PickupLocation, Guid>
    {
        Task<List<PickupLocation>> GetByUserId(string userId);
    }
}
