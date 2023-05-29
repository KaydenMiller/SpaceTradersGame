using System.Text.Json.Serialization;
using SpaceTraders.Core;

namespace SpaceTraders.Pages.Ship;

public class RefuelShipResponse
{
    [JsonPropertyName("agent")]
    public Player.Player Player { get; set; }

    [JsonPropertyName("fuel")]
    public Fuel Fuel { get; set; }

    [JsonPropertyName("transaction")]
    public TradeTransaction Transaction { get; set; }
}