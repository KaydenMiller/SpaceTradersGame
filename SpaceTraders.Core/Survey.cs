using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class Survey
{
    [JsonPropertyName("signature")]
    public string Signature { get; set; }
    
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }
    
    [JsonPropertyName("deposits")]
    public IEnumerable<OreDeposits> OreDeposits { get; set; } = Enumerable.Empty<OreDeposits>();
    
    [JsonPropertyName("expiration")]
    public DateTime Expiration { get; set; }
    
    [JsonPropertyName("size")]
    public string Size { get; set; }
}