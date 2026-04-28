using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.PickupLocation;
using Arzly.Shared.DTOs.Response.PickupLocation;

namespace Arzly.Api.Application.Services
{
    public class PickupLocationService : BaseService<PickupLocation, PickupLocationResponse, PickupLocationAddRequest, PickupLocationUpdateRequest, Guid>, IPickupLocationService
    {
        public PickupLocationService(IPickupLocationRepository repository) : base(repository)
        {
        }

        protected override PickupLocationResponse MapToDto(PickupLocation entity) => entity.ToResponse();
        protected override PickupLocation MapToEntity(PickupLocationAddRequest createDto) => createDto.ToEntity();
        protected override PickupLocation MapToEntity(PickupLocationUpdateRequest updateDto) => updateDto.ToEntity();
    }
}
