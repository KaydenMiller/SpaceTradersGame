using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class Ship
{
    [JsonPropertyName("symbol")]
    public string Id { get; set; }

    [JsonPropertyName("registration")]
    public ShipRegistration Registration { get; set; }

    [JsonPropertyName("nav")]
    public NavigationInfo NavigationInfo { get; set; }

    [JsonPropertyName("crew")]
    public Crew Crew { get; set; }

    [JsonPropertyName("frame")]
    public ShipFrame Frame { get; set; }

    [JsonPropertyName("reactor")]
    public ShipReactor Reactor { get; set; }

    [JsonPropertyName("engine")]
    public ShipEngine Engine { get; set; }

    [JsonPropertyName("modules")]
    public IEnumerable<ShipModule> Modules { get; set; }

    [JsonPropertyName("mounts")]
    public IEnumerable<ShipMount> Mounts { get; set; }

    [JsonPropertyName("cargo")]
    public Cargo Cargo { get; set; }

    [JsonPropertyName("fuel")]
    public Fuel Fuel { get; set; }
}