using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class Extraction
{
    [JsonPropertyName("shipSymbol")]
    public string ShipSymbol { get; set; }

    [JsonPropertyName("yield")]
    public ExtractionYield Yield { get; set; }

    public class ExtractionYield
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        
        [JsonPropertyName("units")]
        public int Units { get; set; }
    }
}