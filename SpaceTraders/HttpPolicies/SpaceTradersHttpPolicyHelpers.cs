using Flurl.Http;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;
using SpaceTraders.Api;
using SpaceTraders.Core;
using SpaceTraders.Pages;

namespace SpaceTraders.HttpPolicies;

public static class SpaceTradersHttpPolicyHelpers
{
    public static IAsyncPolicy RateLimitPolicyAsync = Policy
       .RateLimitAsync(2, TimeSpan.FromSeconds(1), 10);

    public static IAsyncPolicy RateLimitRetryPolicy = Policy
       .Handle<FlurlHttpException>(x => x.StatusCode == StatusCodes.Status429TooManyRequests)
       .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(2), 5));

    public static AsyncRetryPolicy ShipCooldownPolicy()
    {
        return Policy
           .Handle<SpaceTradersApiException>(x => x.ErrorCode == ErrorCodes.COOLDOWN_CONFLICT_ERROR)
           .WaitAndRetryAsync(
                retryCount: 1,
                sleepDurationProvider: (count, ex, context) =>
                {
                    var error = ex as SpaceTradersApiException<ShipCooldown>;
                    return error?.Data?.Expiration - DateTime.UtcNow ?? TimeSpan.FromSeconds(2);
                },
                onRetryAsync: (exception, span, count, ctx) => Task.CompletedTask);
    }
    public static IAsyncPolicy SpaceTradersPolicyAsync()
    {
        return Policy
           .WrapAsync(RateLimitPolicyAsync, RateLimitRetryPolicy);
    }
}