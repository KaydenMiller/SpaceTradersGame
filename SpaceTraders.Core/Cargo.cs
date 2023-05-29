using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class Cargo
{
    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }

    [JsonPropertyName("units")]
    public int TotalUnits { get; set; }

    [JsonPropertyName("inventory")]
    public IEnumerable<InventoryItem> InventoryItems { get; set; } = Enumerable.Empty<InventoryItem>();
}