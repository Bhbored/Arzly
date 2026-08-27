using Microsoft.AspNetCore.SignalR;
using System.Reflection;
using System.Threading.RateLimiting;

namespace Arzly.Api.Filters.HubFilters
{
    /// <summary>
    /// Marker attribute for hub methods that should be rate-limited.
    /// Apply it to any hub method you want throttled per-user.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class HubRateLimitAttribute : Attribute
    {
    }

    /// <summary>
    /// SignalR hub filter that rate-limits methods decorated with <see cref="HubRateLimitAttribute"/>.
    /// Uses a <see cref="PartitionedRateLimiter{TResource}"/> keyed by the caller's user id
    /// (falling back to the connection id when no user is present).
    /// </summary>
    public class RateLimitHubFilter : IHubFilter
    {
        private readonly PartitionedRateLimiter<string> _limiter;

        public RateLimitHubFilter(PartitionedRateLimiter<string> limiter)
        {
            _limiter = limiter;
        }

        public async ValueTask<object?> InvokeMethodAsync(
            HubInvocationContext invocationContext,
            Func<HubInvocationContext, ValueTask<object?>> next)
        {
            // Only throttle methods that explicitly opt in via [HubRateLimit].
            if (invocationContext.HubMethod.GetCustomAttribute<HubRateLimitAttribute>() is not null)
            {
                var key = invocationContext.Context.UserIdentifier
                          ?? invocationContext.Context.ConnectionId;

                using var lease = await _limiter.AcquireAsync(key);
                if (!lease.IsAcquired)
                {
                    throw new HubException("Rate limit exceeded. Please slow down.");
                }
            }

            return await next(invocationContext);
        }
    }
}
