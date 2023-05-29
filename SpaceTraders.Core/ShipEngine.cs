using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class ShipEngine
{
    [JsonPropertyName("symbol")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("condition")]
    public int Condition { get; set; }

    [JsonPropertyName("speed")]
    public int Speed { get; set; }

    [JsonPropertyName("requirements")]
    public Requirements Requirements { get; set; }
}