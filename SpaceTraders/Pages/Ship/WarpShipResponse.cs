using System.Text.Json.Serialization;
using SpaceTraders.Core;

namespace SpaceTraders.Pages.Ship;

public class WarpShipResponse
{
    [JsonPropertyName("nav")]
    public NavigationInfo NavigationInfo { get; set; }

    [JsonPropertyName("fuel")]
    public Fuel Fuel { get; set; }
}