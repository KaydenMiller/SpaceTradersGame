using Flurl;
using Flurl.Http;

namespace SpaceTraders.Pages.Contracts;

public class ContractApiService
{
    private string _token;

    public ContractApiService(IConfiguration configuration)
    {
        _token = configuration["SpaceTraders:ApiKey"]!;
    }
    
    public async Task<IEnumerable<Contract>> GetContracts()
    {
        var response = await SpaceTradersApi.API_ROOT 
           .AppendPathSegment("my")
           .AppendPathSegment("contracts")
           .WithOAuthBearerToken(_token)
           .GetJsonAsync<SpaceTradersArrayResponse<Contract>>();

        return response.Data;
    }

    public async Task<AcceptContractResponse> AcceptContract(string contractId)
    {
        var response = await SpaceTradersApi.API_ROOT 
           .AppendPathSegment("my")
           .AppendPathSegment("contracts")
           .AppendPathSegment(contractId)
           .AppendPathSegment("accept")
           .WithOAuthBearerToken(_token)
           .PostAsync();

        var result = await response.GetJsonAsync<SpaceTradersObjectResponse<AcceptContractResponse>>();

        return result.Data;
    }
}