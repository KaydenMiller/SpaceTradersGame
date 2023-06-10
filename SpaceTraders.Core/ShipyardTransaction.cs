using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class ShipyardTransaction
{
    [JsonPropertyName("agent")]
    public Player Agent { get; set; }

    [JsonPropertyName("ship")]
    public Ship Ship { get; set; }

    [JsonPropertyName("transaction")]
    public ShipTransaction Transaction { get; set; }
}
