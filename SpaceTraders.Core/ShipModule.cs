using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class ShipModule
{
    [JsonPropertyName("symbol")]
    public string Id { get; set; }
    
    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }

    [JsonPropertyName("range")]
    public int Range { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("requirements")]
    public Requirements Requirements { get; set; }
}