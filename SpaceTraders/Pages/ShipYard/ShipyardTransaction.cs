using System.Text.Json.Serialization;
using SpaceTraders.Pages.Location;

namespace SpaceTraders.Pages.ShipYard;

public class ShipyardTransaction
{
    [JsonPropertyName("agent")]
    public Player.Player Agent { get; set; }

    [JsonPropertyName("ship")]
    public Core.Ship Ship { get; set; }

    [JsonPropertyName("transaction")]
    public Transaction Transaction { get; set; }
}
