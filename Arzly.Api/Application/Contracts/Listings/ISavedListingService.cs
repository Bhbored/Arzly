using Arzly.Shared.DTOs.Request.SavedListing;
using Arzly.Shared.DTOs.Response.SavedListing;

namespace Arzly.Api.Application.Contracts.Listings
{
    public interface ISavedListingService
    {
        Task<List<SavedListingResponse>> GetAllAsync(Guid userId);
        Task<SavedListingResponse?> GetByIdAsync(Guid id);
        Task<SavedListingResponse?> CreateAsync(SavedListingAddRequest createDto, Guid userId);
        Task DeleteAsync(Guid id);
        Task UndeleteAsync(Guid id);
    }
}
