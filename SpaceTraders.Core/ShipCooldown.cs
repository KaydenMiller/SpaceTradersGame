using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class ShipCooldown
{
    [JsonPropertyName("shipSymbol")]
    public string ShipId { get; set; } = default!;

    [JsonPropertyName("totalSeconds")]
    public int TotalSeconds { get; set; }

    [JsonPropertyName("remainingSeconds")]
    public int RemainingSeconds { get; set; }

    [JsonPropertyName("expiration")]
    public DateTime? Expiration { get; set; }
    
}