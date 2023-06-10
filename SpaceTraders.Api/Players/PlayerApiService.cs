using Flurl;
using Flurl.Http;

namespace SpaceTraders.Api.Players;

public class PlayerApiService
{
    private readonly SpaceTradersApi _api;

    public PlayerApiService(SpaceTradersApi api)
    {
        _api = api;
    }

    public async Task<Core.Player> GetPlayer()
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("agent")
           .WithOAuthBearerToken(_api.ApiToken)
           .GetJsonAsync<SpaceTradersObjectResponse<Core.Player>>();

        return response.Data;
    }
}