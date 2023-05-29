using Flurl;
using Flurl.Http;

namespace SpaceTraders.Pages.Ship;

public class ShipApiService
{
    private readonly SpaceTradersApi _api;

    public ShipApiService(SpaceTradersApi api)
    {
        _api = api;
    }

    public async Task<NavigateShipResponse> Navigate(Ship ship, Location.Location waypoint)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("ships")
           .AppendPathSegment(ship.Id.Value)
           .AppendPathSegment("navigate")
           .WithOAuthBearerToken(_api.ApiToken)
           .PostJsonAsync(new
            {
                waypointSymbol = waypoint
            });

        return (await response.GetJsonAsync<SpaceTradersObjectResponse<NavigateShipResponse>>()).Data;
    }
}