using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;

namespace Arzly.Api.Application.Contracts
{
    public interface IListingService : IBaseService<Listing, ListingResponse, ListingAddRequest, ListingUpdateRequest, Guid>
    {
    }
}
