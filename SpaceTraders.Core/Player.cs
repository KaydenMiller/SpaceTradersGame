using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class Player
{
    [JsonPropertyName("accountId")]
    public string AccountId { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("headquarters")]
    public string Headquarters { get; set; }

    [JsonPropertyName("credits")]
    public int Credits { get; set; }

    [JsonPropertyName("startingFaction")]
    public string StartingFaction { get; set; }
}