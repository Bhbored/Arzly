using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Response.Listing;
using System.Linq.Expressions;
using System.Text.Json;

namespace Arzly.Api.Domain.Contracts
{
    public interface IListingRepository : IBaseRepository<Listing, Guid>
    {
        Task<List<Listing>> GetFilteredListing(Expression<Func<Listing, bool>> predicate, int pageSize, int currentPage);

        Task<List<Listing>> GetListingByUserId(string id, int pageSize, int currentPage);
        Task<List<Listing>> GetIndexedListings(int pageSzie, int currentPage);
        Task<List<Listing>> GetListingByCategoryId(Guid categoryId, int pageSize, int currentPage);
        Task<List<Listing>> GetInitialListings(Guid categoryId);
        Task AddListingDetails(object details, Guid listingId);
    }
}
