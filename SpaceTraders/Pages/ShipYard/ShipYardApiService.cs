using Flurl;
using Flurl.Http;

namespace SpaceTraders.Pages.ShipYard;

public class ShipYardApiService
{
    public const string API = "https://api.spacetraders.io/v2";
    private string _token;

    public ShipYardApiService(IConfiguration configuration)
    {
        this._token = configuration["SpaceTraders:ApiKey"]!;
    }

    public async Task<ShipYard> GetShipYard(Location.Location location)
    {
        var response = await API
            .AppendPathSegments("systems", location.System, "waypoints", location.Waypoint, "shipyard")
            .WithOAuthBearerToken(this._token)
            .GetJsonAsync<SpaceTradersObjectResponse<ShipYard>>();

        return response.Data;
    }
    
    public async Task<ShipYard> TestCall()
    {
        var response = await API
            .AppendPathSegments("systems", "X1-VS75", "waypoints", "X1-VS75-97637F", "shipyard")
            .WithOAuthBearerToken(this._token)
            .GetJsonAsync<SpaceTradersObjectResponse<ShipYard>>();

        return response.Data;
    }
}