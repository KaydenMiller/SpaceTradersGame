using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class Fuel
{
    [JsonPropertyName("current")]
    public int Current { get; set; }

    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }

    [JsonPropertyName("consumed")]
    public FuelConsumed? Consumed { get; set; }
    
    public class FuelConsumed
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}