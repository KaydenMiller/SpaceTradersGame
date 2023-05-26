using Flurl;
using Flurl.Http;

namespace SpaceTraders.Pages.Player;

public class PlayerApiService
{
    public const string API = "https://api.spacetraders.io/v2";
    private string _token;

    public PlayerApiService(IConfiguration configuration)
    {
        this._token = configuration["SpaceTraders:ApiKey"]!;
    }
    
    public async Task<Player> GetPlayer()
    {
        var response = await API
            .AppendPathSegment("my")
            .AppendPathSegment("agent")
            .WithOAuthBearerToken(this._token)
            .GetJsonAsync<SpaceTradersObjectResponse<Player>>();
        
        return response.Data;
    }
}