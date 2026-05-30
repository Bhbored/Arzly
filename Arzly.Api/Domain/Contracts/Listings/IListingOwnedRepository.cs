namespace Arzly.Api.Domain.Contracts.Listings
{
    public interface IListingOwnedRepository
    {
        Task<object?> GetByListingId(Guid listingId);
        Task<Dictionary<Guid, object>> GetByListingIds(List<Guid> listingIds);
    }
}
