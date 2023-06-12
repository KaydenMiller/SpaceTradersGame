using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class ShipYard
{
    [JsonPropertyName("symbol")]
    [JsonConverter(typeof(LocationJsonConverter))]
    public Location Symbol { get; set; }

    [JsonPropertyName("shipTypes")]
    public IEnumerable<ShipType> ShipTypes { get; set; }
    
    [JsonPropertyName("transactions")]
    public IEnumerable<ShipTransaction> Transactions { get; set; }

    [JsonPropertyName("ships")]
    public IEnumerable<ShipYardShip> Ships { get; set; }
}

public class ShipType
{
    [JsonPropertyName("type")]
    public ShipTypes Type { get; set; }
}

public class ShipTransaction
{
    [JsonPropertyName("waypoinSymbol")]
    public string WaypointSymbol { get; set; }
    
    [JsonPropertyName("shipSymbol")]
    public string ShipSymbol { get; set; }
    
    [JsonPropertyName("price")]
    public int Price { get; set; }
    
    [JsonPropertyName("agentSymbol")]
    public string AgentSymbol { get; set; }
    
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
}