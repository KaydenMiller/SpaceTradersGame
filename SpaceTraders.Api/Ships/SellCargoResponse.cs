using System.Text.Json.Serialization;
using SpaceTraders.Core;

namespace SpaceTraders.Api.Ships;

public class SellCargoResponse
{
    [JsonPropertyName("agent")]
    public Core.Player Player { get; set; }

    [JsonPropertyName("cargo")]
    public Cargo Cargo { get; set; }

    [JsonPropertyName("transaction")]
    public TradeTransaction Transaction { get; set; }
}