using Arzly.Api.Domain.Entities;
using Arzly.Shared.DTOs.Response.Listing;
using Arzly.Shared.Enums;
using System.Linq.Expressions;
using System.Text.Json;

namespace Arzly.Api.Domain.Contracts
{
    public interface IListingRepository : IBaseRepository<Listing, Guid>
    {
        Task<List<Listing>> GetFilteredListing(Expression<Func<Listing, bool>> predicate, int pageSize, int currentPage);

        Task<List<Listing>> GetListingByUserId(string id, int pageSize, int currentPage);
        Task<List<Listing>> GetIndexedListings(int pageSzie, int currentPage);
        Task<List<Listing>> GetListingByCategoryId(Guid categoryId, int pageSize, int currentPage, string? searchString,
            LocationPreset? preset, double minPrice, double maxPrice, string order, string orderByPrice);
        Task<List<Listing>> GetListingBySubCategoryId(Guid subcategoryId, int pageSize, int currentPage, string? searchString,
            LocationPreset? preset, object? details,double minPrice, double maxPrice, string order, string orderByPrice);
        Task<List<Listing>> GetInitialListings(Guid subcategoryId);
        Task AddListingDetails(object details, Guid listingId);

        //admin
        Task<List<Listing>> GetAllListingAdmin(int pageSize, int currentPage);
        Task<Listing> UpdateAdmin(Listing entity);
    }
}
