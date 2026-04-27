using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Mappings;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;

namespace Arzly.Api.Application.Services
{
    public class ListingService : BaseService<Listing, ListingResponse, ListingAddRequest, ListingUpdateRequest, Guid>, IListingService
    {
        public ListingService(IListingRepository repository) : base(repository)
        {
        }

        public override async Task<ListingResponse?> CreateAsync(ListingAddRequest? createDto)
        {
            if (createDto is null)
                throw new ArgumentException("hello");
            //and more
            return await base.CreateAsync(createDto);
        }


        #region Mapping
        protected override ListingResponse MapToDto(Listing entity) =>
            entity.ToResponse();

        protected override Listing MapToEntity(ListingAddRequest createDto) =>
            createDto.ToEntity();

        protected override Listing MapToEntity(ListingUpdateRequest updateDto)
        {
            var entity = new Listing();
            return updateDto.ToEntity(entity);
        }
        #endregion

    }
}
