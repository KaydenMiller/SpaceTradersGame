using Flurl;
using Flurl.Http;

namespace SpaceTraders.Pages.Location;

public class LocationApiService
{
    private readonly SpaceTradersApi _api;

    public LocationApiService(SpaceTradersApi api)
    {
        _api = api;
    }

    public async Task<LocationInfo> GetLocationInfo(Location location)
    {
        var response = await SpaceTradersApi.API_ROOT 
            .AppendPathSegments("systems", location.System, "waypoints", location.Waypoint)
            .WithOAuthBearerToken(_api.ApiToken)
            .GetJsonAsync<SpaceTradersObjectResponse<LocationInfo>>();
        
        return response.Data;
    }

    public async Task<IEnumerable<LocationInfo>> GetSystemInfo(Location location)
    {
        var response = await SpaceTradersApi.API_ROOT 
            .AppendPathSegments("systems", location.System, "waypoints")
            .WithOAuthBearerToken(_api.ApiToken)
            .GetJsonAsync<SpaceTradersArrayResponse<LocationInfo>>();

        return response.Data;
    }
}