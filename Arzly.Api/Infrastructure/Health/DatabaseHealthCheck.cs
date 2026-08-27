using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Arzly.Api.Infrastructure.Health;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly AppDbContext _db;

    public DatabaseHealthCheck(AppDbContext db)
    {
        _db = db;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await _db.Database.CanConnectAsync(cancellationToken)
                ? HealthCheckResult.Healthy("Database connection succeeded")
                : HealthCheckResult.Unhealthy("Database connection failed");
        }
        catch (Exception exception)
        {
            return HealthCheckResult.Unhealthy("Database readiness check failed", exception);
        }
    }
}
