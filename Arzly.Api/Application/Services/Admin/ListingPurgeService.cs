using Arzly.Api.Application.Contracts.Admin;
using Arzly.Api.Domain.Entities.Listings;
using Arzly.Api.Domain.Entities.Users;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Api.Infrastructure.Storage;
using Arzly.Shared.DTOs.Response.Admin;
using Arzly.Shared.Enums.Activity;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Application.Services.Admin;

public sealed class ListingPurgeService : IListingPurgeService
{
    private readonly AppDbContext _db;
    private readonly IImageUploader _imageUploader;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ListingPurgeService> _logger;

    public ListingPurgeService(
        AppDbContext db,
        IImageUploader imageUploader,
        IConfiguration configuration,
        ILogger<ListingPurgeService> logger)
    {
        _db = db;
        _imageUploader = imageUploader;
        _configuration = configuration;
        _logger = logger;
    }

    public Task<int> CountEligibleAsync(CancellationToken cancellationToken = default) =>
        EligibleListings().CountAsync(cancellationToken);

    public async Task<ListingPurgeResultResponse> PurgeExpiredAsync(
        Guid actorId,
        string actorRole,
        int batchSize,
        CancellationToken cancellationToken = default)
    {
        if (actorId == Guid.Empty)
            throw new ArgumentException("A valid purge actor is required");
        if (!await _db.Users.IgnoreQueryFilters().AnyAsync(user => user.Id == actorId, cancellationToken))
            throw new ArgumentException("The configured purge actor does not exist");

        var cutoff = GetCutoffUtc();
        var listings = await EligibleListings()
            .OrderBy(listing => listing.DeletedAt)
            .Take(Math.Clamp(batchSize, 1, 100))
            .ToListAsync(cancellationToken);

        if (listings.Count == 0)
            return new ListingPurgeResultResponse { CutoffUtc = cutoff };

        var images = listings.SelectMany(GetImages).Distinct().ToList();
        var now = DateTime.UtcNow;
        _db.UserActivityLogs.AddRange(listings.Select(listing => new UserActivityLog
        {
            ActorId = actorId,
            ActorRole = actorRole,
            ActionType = ActivityActionType.ListingPurged,
            TargetType = ActivityTargetType.Listing,
            TargetId = listing.Id.ToString(),
            Details = $"Permanently purged after retention cutoff {cutoff:O}",
            Timestamp = now,
            IsSuccess = true
        }));
        _db.Listings.RemoveRange(listings);
        await _db.SaveChangesAsync(cancellationToken);

        var result = new ListingPurgeResultResponse
        {
            CutoffUtc = cutoff,
            PurgedListings = listings.Count
        };
        foreach (var image in images)
        {
            try
            {
                if (await _imageUploader.DeleteFile(
                    image.OwnerId.ToString(), image.Url, cancellationToken))
                    result.DeletedImages++;
            }
            catch (Exception exception)
            {
                result.FailedImageDeletions++;
                _logger.LogError(exception,
                    "Listing was purged but image deletion failed. ListingOwnerId: {OwnerId}",
                    image.OwnerId);
            }
        }

        _logger.LogInformation(
            "Purged {ListingCount} expired listings; deleted {ImageCount} images with {ImageFailures} failures",
            result.PurgedListings, result.DeletedImages, result.FailedImageDeletions);
        return result;
    }

    private IQueryable<Listing> EligibleListings()
    {
        var cutoff = GetCutoffUtc();
        return _db.Listings.IgnoreQueryFilters().Where(listing =>
            listing.IsDeleted && listing.DeletedAt != null && listing.DeletedAt <= cutoff);
    }

    private DateTime GetCutoffUtc()
    {
        var retentionDays = Math.Clamp(
            _configuration.GetValue<int?>("Retention:SoftDeletedListingsDays") ?? 30,
            1,
            3650);
        return DateTime.UtcNow.AddDays(-retentionDays);
    }

    private static IEnumerable<(Guid OwnerId, string Url)> GetImages(Listing listing)
    {
        if (!string.IsNullOrWhiteSpace(listing.PrimaryImageUrl))
            yield return (listing.OwnerId, listing.PrimaryImageUrl);
        foreach (var url in listing.ImagesUrl ?? [])
            if (!string.IsNullOrWhiteSpace(url))
                yield return (listing.OwnerId, url);
    }
}
