using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.PickupLocation;
using Arzly.Shared.DTOs.Response.PickupLocation;

namespace Arzly.Api.Application.Contracts
{
    public interface IPickupLocationService : IBaseService<PickupLocation, PickupLocationResponse, PickupLocationAddRequest, PickupLocationUpdateRequest, Guid>
    {
    }
}
