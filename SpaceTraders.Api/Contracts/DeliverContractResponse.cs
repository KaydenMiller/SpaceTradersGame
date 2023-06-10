using System.Text.Json.Serialization;
using SpaceTraders.Core;

namespace SpaceTraders.Api.Contracts;

public class DeliverContractResponse
{
    [JsonPropertyName("contract")]
    public Contract Contract { get; set; }

    [JsonPropertyName("cargo")]
    public Cargo Cargo { get; set; }
}