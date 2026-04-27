using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.SavedListing;
using Arzly.Shared.DTOs.Response.SavedListing;

namespace Arzly.Api.Application.Services
{
    public class SavedListingService : BaseService<SavedListing, SavedListingResponse, SavedListingAddRequest, SavedListingUpdateRequest, Guid>, ISavedListingService
    {
        public SavedListingService(ISavedListingRepository repository) : base(repository)
        {
        }

        protected override SavedListingResponse MapToDto(SavedListing entity) => entity.ToResponse();
        protected override SavedListing MapToEntity(SavedListingAddRequest createDto) => createDto.ToEntity();
        protected override SavedListing MapToEntity(SavedListingUpdateRequest updateDto) => updateDto.ToEntity();
    }
}
