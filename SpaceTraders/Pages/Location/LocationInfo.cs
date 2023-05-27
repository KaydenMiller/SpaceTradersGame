
using System.Text.Json.Serialization;

namespace SpaceTraders.Pages.Location;

public class LocationInfo
{
    [JsonPropertyName("systemSymbol")]
    public string SystemSymbol { get; set; }
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("x")]
    public int X { get; set; }
    [JsonPropertyName("y")]
    public int Y { get; set; }
    [JsonPropertyName("orbitals")]
    public Orbitals Orbitals { get; set; }
    [JsonPropertyName("traits")]
    public Traits Traits { get; set; }
    [JsonPropertyName("chart")]
    public Chart Chart { get; set; }
    [JsonPropertyName("faction")]
    public Faction Faction { get; set; }
    
}

public class Orbitals
{
    [JsonPropertyName("location")]
    public Location Symbol { get; set; }
}

public class Traits
{
    // name and symbol can probably become enums of some sort
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
}

public class Chart
{
    [JsonPropertyName("submittedBy")]
    public string SubmittedBy { get; set; }
    [JsonPropertyName("submittedOn")]
    public string SubmittedOn { get; set; }
}

public class Faction
{
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }
}