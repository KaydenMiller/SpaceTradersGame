using Flurl;
using Flurl.Http;

namespace SpaceTraders.Pages.ShipYard;

public class ShipYardApiService
{
    private readonly SpaceTradersApi _api;

    public ShipYardApiService(SpaceTradersApi api)
    {
        _api = api;
    }

    public async Task<ShipYard> GetShipYard(Location.Location location)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegments("systems", location.System, "waypoints", location.Waypoint, "shipyard")
           .WithOAuthBearerToken(_api.ApiToken)
           .GetJsonAsync<SpaceTradersObjectResponse<ShipYard>>();

        return response.Data;
    }

    public async Task<ShipYard> TestCall()
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegments("systems", "X1-VS75", "waypoints", "X1-VS75-97637F", "shipyard")
           .WithOAuthBearerToken(_api.ApiToken)
           .GetJsonAsync<SpaceTradersObjectResponse<ShipYard>>();

        return response.Data;
    }
    
    public async Task<ShipyardTransaction> BuyShip(Location.Location location, ShipType shipType)
    {

        var response = await SpaceTradersApi.API_ROOT
            .AppendPathSegments("my", "ships")
            .WithOAuthBearerToken(_api.ApiToken)
            .PostJsonAsync(new
            {
                shipType = shipType,
                waypointSymbol = location.ToString()
            });

        return (await response.GetJsonAsync<SpaceTradersObjectResponse<ShipyardTransaction>>()).Data;
    }
    
}