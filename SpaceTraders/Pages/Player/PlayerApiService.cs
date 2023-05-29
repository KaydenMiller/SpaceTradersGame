using Flurl;
using Flurl.Http;

namespace SpaceTraders.Pages.Player;

public class PlayerApiService
{
    private readonly SpaceTradersApi _api;

    public PlayerApiService(SpaceTradersApi api)
    {
        _api = api;
    }
    
    public async Task<Player> GetPlayer()
    {
        var response = await SpaceTradersApi.API_ROOT 
            .AppendPathSegment("my")
            .AppendPathSegment("agent")
            .WithOAuthBearerToken(_api.ApiToken)
            .GetJsonAsync<SpaceTradersObjectResponse<Player>>();
        
        return response.Data;
    }
}