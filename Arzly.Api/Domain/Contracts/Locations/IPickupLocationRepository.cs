using Arzly.Api.Domain.Entities.Listings;

namespace Arzly.Api.Domain.Contracts.Locations
{
    public interface IPickupLocationRepository : IBaseRepository<PickupLocation, Guid>
    {
        Task<List<PickupLocation>> GetByUserId(Guid userId);

        Task<bool> SoftDeleteLocation(Guid id);
    }
}
