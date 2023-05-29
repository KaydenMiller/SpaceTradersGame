using Flurl;
using Flurl.Http;
using SpaceTraders.Core;

namespace SpaceTraders.Pages.Ship;

public class ShipApiService
{
    private readonly SpaceTradersApi _api;

    public ShipApiService(SpaceTradersApi api)
    {
        _api = api;
    }

    public async Task<NavigateShipResponse> Navigate(Core.Ship ship, Location.Location waypoint)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("ships")
           .AppendPathSegment(ship.Id)
           .AppendPathSegment("navigate")
           .WithOAuthBearerToken(_api.ApiToken)
           .PostJsonAsync(new
            {
                waypointSymbol = waypoint
            });

        return (await response.GetJsonAsync<SpaceTradersObjectResponse<NavigateShipResponse>>()).Data;
    }

    public async Task<Cargo> GetShipCargo(Core.Ship ship)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("ships")
           .AppendPathSegment(ship.Id)
           .AppendPathSegment("cargo")
           .WithOAuthBearerToken(_api.ApiToken)
           .GetJsonAsync<SpaceTradersObjectResponse<Cargo>>();

        return response.Data;
    }

    public async Task<NavigationInfo> OrbitCurrent(Core.Ship ship)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("ships")
           .AppendPathSegment(ship.Id)
           .AppendPathSegment("orbit")
           .WithOAuthBearerToken(_api.ApiToken)
           .GetJsonAsync<SpaceTradersObjectResponse<NavigationInfo>>();

        return response.Data;
    }

    public async Task<ShipCooldown> GetShipCooldown(Core.Ship ship)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("ships")
           .AppendPathSegment(ship.Id)
           .AppendPathSegment("cooldown")
           .WithOAuthBearerToken(_api.ApiToken)
           .GetJsonAsync<SpaceTradersObjectResponse<ShipCooldown>>();

        return response.Data;
    }
}