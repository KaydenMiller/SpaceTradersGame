using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class ShipRegistration
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("factionSymbol")]
    public string FactionSymbol { get; set; }

    [JsonPropertyName("role")]
    public ShipRole Role { get; set; }
}