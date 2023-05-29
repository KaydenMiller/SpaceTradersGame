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
    public OreDeposits ValidOreDeposits { get; set; }

    [JsonPropertyName("requirements")]
    public MountRequirements Requirements { get; set; }

    public class MountRequirements
    {
        [JsonPropertyName("power")]
        public int Power { get; set; }

        [JsonPropertyName("crew")]
        public int Crew { get; set; }

        [JsonPropertyName("slots")]
        public int Slots { get; set; }
    }
}