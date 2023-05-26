using System.Text.Json.Serialization;

namespace SpaceTraders.Pages.Contracts;

public class Contract
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("factionSymbol")] 
    public string FactionSymbol { get; set; } = default!;

    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;


    public class ContractTerms
    {
        [JsonPropertyName("deadline")]
        public DateTime Deadline { get; set; }

        [JsonPropertyName("payment")]
        public ContractPayment Payment { get; set; }
        
        public class ContractPayment
        {
            [JsonPropertyName("onAccepted")]
            public int OnAccepted { get; set; }

            [JsonPropertyName("onFulfilled")]
            public int OnFulfilled { get; set; }
        }
    }
}