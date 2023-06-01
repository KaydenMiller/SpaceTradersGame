using System.Text.Json.Serialization;
using System.Transactions;

namespace SpaceTraders.Core;

public class Market
{
    [JsonPropertyName("symbol")]
    public string Id { get; set; }

    [JsonPropertyName("exports")]
    public IEnumerable<TradeGood> Exports { get; set; }

    [JsonPropertyName("imports")]
    public IEnumerable<TradeGood> Imports { get; set; }

    [JsonPropertyName("exchange")]
    public IEnumerable<TradeGood> Exchanges { get; set; }

    [JsonPropertyName("transactions")]
    public IEnumerable<TradeTransaction> Transactions { get; set; }

    [JsonPropertyName("tradeGoods")]
    public IEnumerable<MarketTradeGood> TradeGoods { get; set; }
}