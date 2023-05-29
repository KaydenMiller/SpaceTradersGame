using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class Requirements
{
    [JsonPropertyName("power")]
    public int Power { get; set; }

    [JsonPropertyName("crew")]
    public int Crew { get; set; }

    [JsonPropertyName("slots")]
    public int Slots { get; set; }
}