using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using SpaceTraders.Core;

namespace SpaceTraders.Api.Contracts;

public class ContractApiService
{
    private readonly string _token;

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


    public async Task<DeliverContractResponse> DeliverCargoForContract(string contractId, Core.Ship shipToDeliverFrom, string cargoItemId, int quantityToDeliver)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("contracts")
           .AppendPathSegment(contractId)
           .AppendPathSegment("deliver")
           .PostJsonAsync(new
            {
                shipSymbol = shipToDeliverFrom.Id,
                tradeSymbol = cargoItemId,
                units = quantityToDeliver
            });

        var result = await response.GetJsonAsync<SpaceTradersObjectResponse<DeliverContractResponse>>();

        return result.Data;
    }

    public async Task<FulfillContractResponse> FulfillContract(string contractId)
    {
        var response = await SpaceTradersApi.API_ROOT
           .AppendPathSegment("my")
           .AppendPathSegment("contracts")
           .AppendPathSegment(contractId)
           .AppendPathSegment("fulfill")
           .PostAsync();
        
        var result = await response.GetJsonAsync<SpaceTradersObjectResponse<FulfillContractResponse>>();

        return result.Data; 
    }
}