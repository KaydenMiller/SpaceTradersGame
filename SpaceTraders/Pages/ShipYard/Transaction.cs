using System.Text.Json.Serialization;
using SpaceTraders.Core;

namespace SpaceTraders.Pages.ShipYard;

public class Transaction
{
    [JsonPropertyName("shipSymbol")]
    public ShipTypes ShipSymbol { get; set; }

    [JsonPropertyName("waypointSymbol")]
    public Core.Location WaypointsSymbol { get; set; }

    [JsonPropertyName("agentSymbol")]
    public string AgentSymbol { get; set; }

    [JsonPropertyName("price")]
    public int Price { get; set; }

    [JsonPropertyName("timeStamp")]
    public DateTime TimeStamp { get; set; }
}