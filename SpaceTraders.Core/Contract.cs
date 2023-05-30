using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class Contract
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("factionSymbol")]
    public string FactionSymbol { get; set; } = default!;

    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("accepted")]
    public bool Accepted { get; set; }

    [JsonPropertyName("fulfilled")]
    public bool Fulfilled { get; set; }

    [JsonPropertyName("expiration")]
    public DateTime Expiration { get; set; }

    [JsonPropertyName("deadlineToAccept")]
    public DateTime DeadlineToAccept { get; set; }

    [JsonPropertyName("terms")]
    public ContractTerms Terms { get; set; } = default!;


    public class ContractTerms
    {
        [JsonPropertyName("deadline")]
        public DateTime Deadline { get; set; }

        [JsonPropertyName("payment")]
        public ContractPayment Payment { get; set; } = default!;

        [JsonPropertyName("deliver")]
        public IEnumerable<Delivery>? Deliveries { get; set; }

        public class ContractPayment
        {
            [JsonPropertyName("onAccepted")]
            public int OnAccepted { get; set; }

            [JsonPropertyName("onFulfilled")]
            public int OnFulfilled { get; set; }
        }

        public class Delivery
        {
            [JsonPropertyName("tradeSymbol")]
            public string TradeSymbol { get; set; }

            [JsonPropertyName("destinationSymbol")]
            public string DestinationSymbol { get; set; }

            [JsonPropertyName("unitsRequired")]
            public int UnitsRequired { get; set; }

            [JsonPropertyName("unitsFulfilled")]
            public int UnitsFulfilled { get; set; }
        }
    }
}