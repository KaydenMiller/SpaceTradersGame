using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class Crew
{
    [JsonPropertyName("current")]
    public int Current { get; set; }
    
    [JsonPropertyName("required")]
    public int Required { get; set; }
    
    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }
    
    [JsonPropertyName("rotation")]
    public string Rotation { get; set; }
    
    [JsonPropertyName("morale")]
    public int Morale { get; set; }
    
    [JsonPropertyName("wages")]
    public int Wages { get; set; }
}