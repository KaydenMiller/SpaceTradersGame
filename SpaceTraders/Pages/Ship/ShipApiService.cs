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

    public async Task<Core.Ship> GetShipDetail(string shipSymbol)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("ships")
           .AppendPathSegment(shipSymbol)
           .WithOAuthBearerToken(_api.ApiToken)
           .GetJsonAsync<SpaceTradersObjectResponse<Core.Ship>>();

        return response.Data;
    }

    public async Task<IEnumerable<Core.Ship>> GetOwnedShips()
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("ships")
           .WithOAuthBearerToken(_api.ApiToken)
           .GetJsonAsync<SpaceTradersArrayResponse<Core.Ship>>();

        return response.Data; 
    }

    public async Task<NavigateShipResponse> Navigate(Core.Ship ship, Core.Location waypoint)
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
           .PostAsync();
            
        var result = await response.GetJsonAsync<SpaceTradersObjectResponse<NavigationInfo>>();

        return result.Data;
    }

    public async Task<NavigationInfo> DockCurrent(Core.Ship ship)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("ships")
           .AppendPathSegment(ship.Id)
           .AppendPathSegment("dock")
           .WithOAuthBearerToken(_api.ApiToken)
           .PostAsync();
            
        var result = await response.GetJsonAsync<SpaceTradersObjectResponse<NavigationInfo>>();

        return result.Data;
    }

    public async Task<RefuelShipResponse> Refuel(Core.Ship ship)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("ships")
           .AppendPathSegment(ship.Id)
           .AppendPathSegment("refuel")
           .WithOAuthBearerToken(_api.ApiToken)
           .PostAsync();

        var result = await response.GetJsonAsync<SpaceTradersObjectResponse<RefuelShipResponse>>();

        return result.Data;  
    }

    public async Task<ShipCooldown?> GetShipCooldown(Core.Ship ship)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("ships")
           .AppendPathSegment(ship.Id)
           .AppendPathSegment("cooldown")
           .WithOAuthBearerToken(_api.ApiToken)
           .GetJsonAsync<SpaceTradersObjectResponse<ShipCooldown>?>();

        return response?.Data;
    }

    public async Task<WarpShipResponse> Warp(Core.Ship ship, Core.Location waypoint)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("ships")
           .AppendPathSegment(ship.Id)
           .AppendPathSegment("warp")
           .WithOAuthBearerToken(_api.ApiToken)
           .PostJsonAsync(new
            {
                waypointSymbol = waypoint.ToString()
            });
            
        var result = await response.GetJsonAsync<SpaceTradersObjectResponse<WarpShipResponse>>();

        return result.Data;
    }

    public async Task<ExtractionResponse> ExtractOre(Core.Ship ship, Survey? survey = null)
    {
        var request = SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("ships")
           .AppendPathSegment(ship.Id)
           .AppendPathSegment("extract")
           .WithOAuthBearerToken(_api.ApiToken);

        IFlurlResponse response; 

        if (survey is not null)
        {
            response = await request.PostJsonAsync(survey);
        }
        else
        {
            response = await request.PostAsync();
        }
            
        var result = await response.GetJsonAsync<SpaceTradersObjectResponse<ExtractionResponse>>();
        return result.Data;
    }

    public async Task<SurveyResponse> Survey(Core.Ship ship)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("ships")
           .AppendPathSegment(ship.Id)
           .AppendPathSegment("survey")
           .WithOAuthBearerToken(_api.ApiToken)
           .PostAsync();
            
        var result = await response.GetJsonAsync<SpaceTradersObjectResponse<SurveyResponse>>();

        return result.Data; 
    }
}