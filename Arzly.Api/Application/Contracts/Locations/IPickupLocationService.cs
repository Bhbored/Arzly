using Arzly.Api.Domain.Entities.Listings;
using Arzly.Shared.DTOs.Request.PickupLocation;
using Arzly.Shared.DTOs.Response.PickupLocation;

namespace Arzly.Api.Application.Contracts.Locations
{
    public interface IPickupLocationService
    {
        Task<List<PickupLocationResponse>> GetAllAsync();
        Task<PickupLocationResponse?> CreateAsync(PickupLocationAddRequest? request, Guid userId);
        Task<PickupLocationResponse?> UpdateAsync(PickupLocationUpdateRequest? request, Guid userId);
        Task<List<PickupLocationResponse>> GetByUserId(Guid? userId);
        Task<PickupLocationResponse?> GetByIdAsync(Guid id, Guid userId);
        Task<bool> SoftDeleteLocation(Guid id, Guid userId);
    }
}
