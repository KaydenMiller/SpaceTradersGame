using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class TradeTransaction
{
    [JsonPropertyName("waypointSymbol")]
    public string WaypointSymbol { get; set; }
    
    [JsonPropertyName("shipSymbol")]
    public string ShipSymbol { get; set; }
    
    [JsonPropertyName("tradeSymbol")]
    public string TradeSymbol { get; set; }
    
    [JsonPropertyName("type")]
    public TradeTransactionType Type { get; set; }
    
    [JsonPropertyName("units")]
    public int Units { get; set; }

    [JsonPropertyName("pricePerUnit")]
    public int PricePerUnit { get; set; }

    [JsonPropertyName("totalPrice")]
    public int TotalPrice { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
}