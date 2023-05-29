using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class ShipFrame
{
    [JsonPropertyName("symbol")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("condition")]
    public int Condition { get; set; }

    [JsonPropertyName("moduleSlots")]
    public int ModuleSlots { get; set; }

    [JsonPropertyName("mountingPoints")]
    public int MountingPoints { get; set; }

    [JsonPropertyName("fuelCapacity")]
    public int FuelCapacity { get; set; }

    [JsonPropertyName("requirements")]
    public Requirements Requirements { get; set; }
}