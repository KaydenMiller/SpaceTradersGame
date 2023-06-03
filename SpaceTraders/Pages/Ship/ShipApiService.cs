using System.Diagnostics;
using Flurl;
using Flurl.Http;
using SpaceTraders.Core;

namespace SpaceTraders.Pages.Ship;

public class ShipApiService
{
    private readonly SpaceTradersApi _api;
    private readonly ILogger<ShipApiService> _logger;

    public List<Survey> Surveys { get; set; } = new();

    public ShipApiService(SpaceTradersApi api, ILogger<ShipApiService> logger)
    {
        _api = api;
        _logger = logger;
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

        try
        {
            if (survey is not null)
            {
                response = await request.PostJsonAsync(new
                {
                    survey
                });
            }
            else
            {
                response = await request.PostAsync();
            }
        }
        catch (FlurlHttpException fhe) when (fhe.StatusCode == StatusCodes.Status409Conflict)
        {
            var error = await fhe.GetResponseJsonAsync<SpaceTradersErrorResponse>();
            switch (error.Error.ErrorCode)
            {
                case ErrorCodes.COOLDOWN_CONFLICT_ERROR:
                    var betterError = await fhe.GetResponseJsonAsync<SpaceTradersErrorResponse<SpaceTradersObjectResponse<ShipCooldown>>>();
                    throw new SpaceTradersApiException<ShipCooldown>(error.Error.Message, error.Error.ErrorCode, betterError.Error.Data?.Data);
                case ErrorCodes.UNKNOWN_ERROR:
                    throw new SpaceTradersApiException("The error we received from the api was not one we know about",
                        ErrorCodes.UNKNOWN_ERROR);
                default:
                    throw new UnreachableException();
            }
        }
            
        var result = await response.GetJsonAsync<SpaceTradersObjectResponse<ExtractionResponse>>();
        return result.Data;
    }

    public async Task<SellCargoResponse> SellCargo(Core.Ship ship, string cargoSymbol, int unitsToSell)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("ships")
           .AppendPathSegment(ship.Id)
           .AppendPathSegment("sell")
           .WithOAuthBearerToken(_api.ApiToken)
           .PostJsonAsync(new
            {
                symbol = cargoSymbol,
                units = unitsToSell
            });
            
        var result = await response.GetJsonAsync<SpaceTradersObjectResponse<SellCargoResponse>>();

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
        
        Surveys.AddRange(result.Data.Surveys);

        return result.Data; 
    }

    public async Task<Cargo> JettisonCargo(Core.Ship ship, string cargoSymbol, int quantityToJettison)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("ships")
           .AppendPathSegment(ship.Id)
           .AppendPathSegment("jettison")
           .WithOAuthBearerToken(_api.ApiToken)
           .PostJsonAsync(new
            {
                symbol = cargoSymbol,
                units = quantityToJettison
            });

        var result = await response.GetJsonAsync<SpaceTradersObjectResponse<Cargo>>();

        return result.Data;
    }
}