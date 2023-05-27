
using System.Text.Json.Serialization;

namespace SpaceTraders.Pages.Location;

public class LocationInfo
{
    [JsonPropertyName("systemSymbol")] 
    public string SystemSymbol { get; set; }
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }
    // Type can probably me a enum of some kind: PLANT, MOON, ORBITAL STATION
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("x")]
    public int X { get; set; }
    [JsonPropertyName("y")]
    public int Y { get; set; }
    [JsonPropertyName("orbitals")]
    public IEnumerable<Orbital> Orbitals { get; set; }
    [JsonPropertyName("traits")]
    public IEnumerable<Trait> Traits { get; set; }
    [JsonPropertyName("chart")]
    public Chart Chart { get; set; }
    [JsonPropertyName("faction")]
    public Faction Faction { get; set; }
    
}

public class Orbital
{
    private Location _symbol;
    
    [JsonPropertyName("symbol")]
    [JsonConverter(typeof(LocationJsonConverter))]
    public Location Symbol { get; set; }
}

public class Trait
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