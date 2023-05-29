using Flurl;
using Flurl.Http;
using SpaceTraders.Core;

namespace SpaceTraders.Pages.ShipYard;

public class ShipYardApiService
{
    private readonly SpaceTradersApi _api;

    public ShipYardApiService(SpaceTradersApi api)
    {
        _api = api;
    }

    public async Task<ShipYard> GetShipYard(Core.Location location)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegments("systems", location.System, "waypoints", location.Waypoint, "shipyard")
           .WithOAuthBearerToken(_api.ApiToken)
           .GetJsonAsync<SpaceTradersObjectResponse<ShipYard>>();

        return response.Data;
    }
    
    public async Task<ShipyardTransaction> BuyShip(Core.Location location, ShipTypes shipType)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegments("my", "ships")
           .WithOAuthBearerToken(_api.ApiToken)
           .PostJsonAsync(new
            {
                shipType = shipType.ToString(),
                waypointSymbol = location.ToString()
            });
        
        return (await response.GetJsonAsync<SpaceTradersObjectResponse<ShipyardTransaction>>()).Data;
    }
    
}