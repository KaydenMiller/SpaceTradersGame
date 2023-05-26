using Flurl;
using Flurl.Http;

namespace SpaceTraders.Pages.Location;

public class LocationApiService
{
    public const string API = "https://api.spacetraders.io/v2";
    private string _token;

    public LocationApiService(IConfiguration configuration)
    {
        this._token = configuration["SpaceTraders:ApiKey"]!;
    }

    public async Task<Location> GetLocationInfo(Waypoint waypoint)
    {
        var response = await API
            .AppendPathSegments("systems", waypoint.System, "waypoints", waypoint.Location)
            .WithOAuthBearerToken(this._token)
            .GetJsonAsync<SpaceTradersObjectResponse<Location>>();
        
        return response.Data;
    }
}