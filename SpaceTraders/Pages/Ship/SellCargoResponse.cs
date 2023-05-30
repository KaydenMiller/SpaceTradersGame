using System.Text.Json.Serialization;
using SpaceTraders.Core;

namespace SpaceTraders.Pages.Ship;

public class SellCargoResponse
{
    [JsonPropertyName("agent")]
    public Player.Player Player { get; set; }

    [JsonPropertyName("cargo")]
    public Cargo Cargo { get; set; }

    [JsonPropertyName("transaction")]
    public TradeTransaction Transaction { get; set; }
}