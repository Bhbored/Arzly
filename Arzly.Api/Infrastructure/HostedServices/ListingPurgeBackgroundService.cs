using Arzly.Api.Application.Contracts.Admin;

namespace Arzly.Api.Infrastructure.HostedServices;

public sealed class ListingPurgeBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ListingPurgeBackgroundService> _logger;

    public ListingPurgeBackgroundService(
        IServiceScopeFactory scopeFactory,
        IConfiguration configuration,
        ILogger<ListingPurgeBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _configuration = configuration;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!_configuration.GetValue<bool>("Retention:ScheduledPurgeEnabled"))
            return;

        while (!stoppingToken.IsCancellationRequested)
        {
            await RunOnceAsync(stoppingToken);
            var intervalHours = Math.Clamp(
                _configuration.GetValue<int?>("Retention:PurgeIntervalHours") ?? 24, 1, 168);
            await Task.Delay(TimeSpan.FromHours(intervalHours), stoppingToken);
        }
    }

    private async Task RunOnceAsync(CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(_configuration["Retention:SystemActorId"], out var actorId))
        {
            _logger.LogError("Scheduled listing purge is enabled but Retention:SystemActorId is invalid");
            return;
        }

        try
        {
            using var scope = _scopeFactory.CreateScope();
            var purgeService = scope.ServiceProvider.GetRequiredService<IListingPurgeService>();
            await purgeService.PurgeExpiredAsync(actorId, "system", 100, cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Scheduled listing purge failed");
        }
    }
}
