using System.Text.Json.Serialization;
using SpaceTraders.Core;

namespace SpaceTraders.Api.Ships;

public class RefuelShipResponse
{
    [JsonPropertyName("agent")]
    public Core.Player Player { get; set; }

    [JsonPropertyName("fuel")]
    public Fuel Fuel { get; set; }

    [JsonPropertyName("transaction")]
    public TradeTransaction Transaction { get; set; }
}