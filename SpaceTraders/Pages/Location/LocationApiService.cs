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

    public async Task<LocationInfo> GetLocationInfo(Location location)
    {
        var response = await API
            .AppendPathSegments("systems", location.System, "waypoints", location.Waypoint)
            .WithOAuthBearerToken(this._token)
            .GetJsonAsync<SpaceTradersObjectResponse<LocationInfo>>();
        
        return response.Data;
    }
}