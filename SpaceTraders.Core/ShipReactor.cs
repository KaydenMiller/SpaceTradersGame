using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class ShipReactor
{
    [JsonPropertyName("symbol")]
    public string Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("condition")]
    public int Condition { get; set; }
    
    [JsonPropertyName("powerOutput")]
    public int PowerOutput { get; set; }
    
    [JsonPropertyName("requirements")]
    public Requirements Requirements { get; set; }
}