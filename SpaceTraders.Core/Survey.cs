using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class Survey
{
    [JsonPropertyName("signature")]
    public string Signature { get; set; }
    
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }
    
    [JsonPropertyName("deposits")]
    public IEnumerable<OreDeposit> OreDeposits { get; set; } = Enumerable.Empty<OreDeposit>();
    
    [JsonPropertyName("expiration")]
    public DateTime Expiration { get; set; }
    
    [JsonPropertyName("size")]
    public string Size { get; set; }

    public class OreDeposit
    {
        [JsonPropertyName("symbol")]
        public OreDeposits Deposit { get; set; }
    }
}