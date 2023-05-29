using System.Text.Json.Serialization;
using SpaceTraders.Core;

namespace SpaceTraders.Pages.Ship;

public class NavigateShipResponse
{
    [JsonPropertyName("fuel")]
    public Fuel Fuel { get; set; } = default!;

    [JsonPropertyName("nav")]
    public NavigationInfo NavigationInfo { get; set; } = default!;
}