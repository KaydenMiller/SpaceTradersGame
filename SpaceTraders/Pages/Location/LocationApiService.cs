using Flurl;
using Flurl.Http;
using SpaceTraders.Core;

namespace SpaceTraders.Pages.Location;

public class LocationApiService
{
    private readonly SpaceTradersApi _api;

    public IEnumerable<MarketTradeGood>? MarketTradeGoods { get; private set; }

    public LocationApiService(SpaceTradersApi api)
    {
        _api = api;
    }

    public async Task<LocationInfo> GetLocationInfo(Core.Location location)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegments("systems", location.System, "waypoints", location.Waypoint)
           .WithOAuthBearerToken(_api.ApiToken)
           .GetJsonAsync<SpaceTradersObjectResponse<LocationInfo>>();

        return response.Data;
    }

    public async Task<IEnumerable<LocationInfo>> GetSystemInfo(Core.Location location)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegments("systems", location.System, "waypoints")
           .WithOAuthBearerToken(_api.ApiToken)
           .GetJsonAsync<SpaceTradersArrayResponse<LocationInfo>>();

        return response.Data;
    }

    // we have two of these...
    public async Task<Core.Market> GetMarketPlace(Core.Location systemSymbol,
        Core.Location marketWaypointSymbol)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("systems")
           .AppendPathSegment(systemSymbol.System)
           .AppendPathSegment("waypoints")
           .AppendPathSegment(marketWaypointSymbol)
           .AppendPathSegment("market")
           .WithOAuthBearerToken(_api.ApiToken)
           .GetJsonAsync<SpaceTradersObjectResponse<Core.Market>>();

        MarketTradeGoods = response.Data.TradeGoods;

        return response.Data;
    }
}