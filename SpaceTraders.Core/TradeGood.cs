using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class TradeGood
{
    [JsonPropertyName("symbol")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;
}