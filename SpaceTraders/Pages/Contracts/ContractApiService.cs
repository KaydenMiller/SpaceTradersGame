using Flurl;
using Flurl.Http;

namespace SpaceTraders.Pages.Contracts;

public class ContractApiService
{
    public const string API = "https://api.spacetraders.io/v2";
    public const string TOKEN = "";
    public async Task<IEnumerable<Contract>> GetContracts()
    {
        var response = await API
           .AppendPathSegment("my")
           .AppendPathSegment("contracts")
           .WithOAuthBearerToken(TOKEN)
           .GetJsonAsync<SpaceTradersResponse<Contract>>();

        return response.Data;
    }
}