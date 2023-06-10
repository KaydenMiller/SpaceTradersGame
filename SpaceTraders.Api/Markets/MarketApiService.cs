using Flurl;
using Flurl.Http;

namespace SpaceTraders.Api.Markets;

public class MarketApiService
{
    private readonly SpaceTradersApi _api;

    public MarketApiService(SpaceTradersApi api)
    {
        _api = api;
    }

    public async Task<Core.Market> GetMarketInfo(Core.Location location)
    {
        var response = await SpaceTradersApi.API_ROOT
            .AppendPathSegments("systems", location.System, "waypoints", location.Waypoint, "market")
            .WithOAuthBearerToken(_api.ApiToken)
            .GetJsonAsync<SpaceTradersObjectResponse<Core.Market>>();

        return response.Data;
    }
}