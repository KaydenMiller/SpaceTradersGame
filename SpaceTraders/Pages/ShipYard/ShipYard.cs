using System.Text.Json.Serialization;
using SpaceTraders.Core;
using SpaceTraders.Pages.Location;

namespace SpaceTraders.Pages.ShipYard;

public class ShipYard
{
    [JsonPropertyName("symbol")]
    [JsonConverter(typeof(LocationJsonConverter))]
    public Core.Location Symbol { get; set; }

    [JsonPropertyName("shipTypes")]
    public IEnumerable<ShipType> Ships { get; set; }
}

public class ShipType
{
    [JsonPropertyName("type")]
    public ShipTypes Type { get; set; }
}