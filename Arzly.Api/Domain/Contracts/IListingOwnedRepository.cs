namespace Arzly.Api.Domain.Contracts
{
    public interface IListingOwnedRepository
    {
        Task<object?> GetByListingId(Guid listingId);
        Task<Dictionary<Guid, object>> GetByListingIds(List<Guid> listingIds);
    }
}
