using Arzly.Shared.DTOs.Response.Admin;

namespace Arzly.Api.Application.Contracts.Admin;

public interface IListingPurgeService
{
    Task<int> CountEligibleAsync(CancellationToken cancellationToken = default);
    Task<ListingPurgeResultResponse> PurgeExpiredAsync(
        Guid actorId,
        string actorRole,
        int batchSize,
        CancellationToken cancellationToken = default);
}
