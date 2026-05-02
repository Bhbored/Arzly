using Arzly.Api.Domain.Entities;
using System.Linq.Expressions;

namespace Arzly.Api.Domain.Contracts
{
    public interface IListingRepository : IBaseRepository<Listing, Guid>
    {
        Task<List<Listing>> GetFilteredListing(Expression<Func<Listing, bool>> predicate);

        Task<List<Listing>> GetListingByUserId(string id);
        Task<List<Listing>> GetIndexedListings(int pageSzie, int currentPage);
    }
}
