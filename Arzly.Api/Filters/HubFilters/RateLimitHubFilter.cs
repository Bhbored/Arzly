using Microsoft.AspNetCore.SignalR;
using System.Reflection;
using System.Threading.RateLimiting;

namespace Arzly.Api.Filters.HubFilters
{

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class HubRateLimitAttribute : Attribute
    {
    }


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
