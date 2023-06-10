using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class ShipYardShip
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