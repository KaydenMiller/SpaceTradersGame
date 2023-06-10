using System.Text.Json.Serialization;
using SpaceTraders.Core;

namespace SpaceTraders.Api.Contracts;

public class FulfillContractResponse
{
    [JsonPropertyName("player")]
    public Player Player { get; set; }

    [JsonPropertyName("contract")]
    public Contract Contract { get; set; }
}