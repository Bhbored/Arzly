using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.PickupLocation;
using Arzly.Shared.DTOs.Response.PickupLocation;

namespace Arzly.Api.Application.Contracts.Locations
{
    public interface IPickupLocationService : IBaseService<PickupLocation, PickupLocationResponse, PickupLocationAddRequest, PickupLocationUpdateRequest, Guid>
    {
        Task<List<PickupLocationResponse>> GetByUserId(Guid? userId);
        Task<bool> SoftDeleteLocation(Guid id, string? userId);
    }
}
