using System.Text.Json.Serialization;
using SpaceTraders.Core;
using SpaceTraders.Pages.Location;

namespace SpaceTraders.Pages.ShipYard;

public class ShipYard
{
    [JsonPropertyName("symbol")]
    [JsonConverter(typeof(LocationJsonConverter))]
    public Core.Location Symbol { get; set; }

    [JsonPropertyName("shipTypes")]
    public IEnumerable<ShipType> ShipTypes { get; set; }
    
    [JsonPropertyName("transactions")]
    public IEnumerable<ShipTransaction> Transactions { get; set; }

    [JsonPropertyName("ships")]
    public IEnumerable<Ship> Ships { get; set; }
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

public class Ship
{
    [JsonPropertyName("type")]
    public ShipTypes Type { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("purchasePrice")]
    public int PurchasePrice { get; set; }
}