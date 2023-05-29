using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class InventoryItem
{
    [JsonPropertyName("symbol")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    /// <summary>
    ///     This should always be >= 1 if the object exists in the inventory
    /// </summary>
    [JsonPropertyName("units")]
    public int Quantity { get; set; }
}