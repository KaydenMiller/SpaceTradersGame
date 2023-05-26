using Flurl;
using Flurl.Http;

namespace SpaceTraders.Pages.Contracts;

public class ContractApiService
{
    public const string API = "https://api.spacetraders.io/v2";

    private string _token;

    public ContractApiService(IConfiguration configuration)
    {
        _token = configuration["SpaceTraders:ApiKey"]!;
    }
    
    public async Task<IEnumerable<Contract>> GetContracts()
    {
        var response = await API
           .AppendPathSegment("my")
           .AppendPathSegment("contracts")
           .WithOAuthBearerToken(_token)
           .GetJsonAsync<SpaceTradersArrayResponse<Contract>>();

        return response.Data;
    }
}