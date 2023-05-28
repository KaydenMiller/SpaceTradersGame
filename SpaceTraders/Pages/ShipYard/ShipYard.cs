using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using MudBlazor;
using SpaceTraders.Pages.Location;

namespace SpaceTraders.Pages.ShipYard;

public class ShipYard
{
    [JsonPropertyName("symbol")]
    [JsonConverter(typeof(LocationJsonConverter))]
    public Location.Location Symbol { get; set; }
    [JsonPropertyName("shipTypes")]
    public IEnumerable<ShipType> Ships { get; set; }
}

public class ShipType
{
    [JsonPropertyName("type")]
    public ShipTypes Type { get; set; }
}

// API returns names in this format
[SuppressMessage("ReSharper", "InconsistentNaming")]
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ShipTypes
{
    [EnumMember(Value = "SHIP_PROBE")]
    SHIP_PROBE,
    [EnumMember(Value = "SHIP_MINING_DRONE")]
    SHIP_MINING_DRONE,
    [EnumMember(Value = "SHIP_INTERCEPTOR")]
    SHIP_INTERCEPTOR,
    [EnumMember(Value = "SHIP_LIGHT_HAULER")]
    SHIP_LIGHT_HAULER,
    [EnumMember(Value = "SHIP_COMMAND_FRIGATE")]
    SHIP_COMMAND_FRIGATE,
    [EnumMember(Value = "SHIP_EXPLORER")]
    SHIP_EXPLORER,
    [EnumMember(Value = "SHIP_HEAVY_FREIGHTER")]
    SHIP_HEAVY_FREIGHTER,
    [EnumMember(Value = "SHIP_LIGHT_SHUTTLE")]
    SHIP_LIGHT_SHUTTLE,
    [EnumMember(Value = "SHIP_ORE_HOUND")]
    SHIP_ORE_HOUND,
    [EnumMember(Value = "SHIP_REFINING_FREIGHTER")]
    SHIP_REFINING_FREIGHTER
}