using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class ShipMount
{
    [JsonPropertyName("symbol")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("strength")]
    public int Strength { get; set; }

    [JsonPropertyName("deposits")]
    public IEnumerable<OreDeposits> ValidOreDeposits { get; set; }

    [JsonPropertyName("requirements")]
    public Requirements Requirements { get; set; }
}