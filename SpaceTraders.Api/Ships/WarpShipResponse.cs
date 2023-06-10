using System.Text.Json.Serialization;
using SpaceTraders.Core;

namespace SpaceTraders.Api.Ships;

public class WarpShipResponse
{
    [JsonPropertyName("nav")]
    public NavigationInfo NavigationInfo { get; set; }

    [JsonPropertyName("fuel")]
    public Fuel Fuel { get; set; }
}