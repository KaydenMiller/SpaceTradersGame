using System.Text.Json.Serialization;

namespace SpaceTraders.Core;

public class MarketTradeGood
{
    /// <summary>
    /// The symbol of the trade good
    /// </summary>
    [JsonPropertyName("symbol")]
    public string Id { get; set; } = default!;

    /// <summary>
    /// The typical volume flowing through the market for this type of good.
    /// The larger the volume, the more stable the price will be.
    /// </summary>
    [JsonPropertyName("tradeVolume")]
    public int TradeVolume { get; set; }

    /// <summary>
    /// A rough estimate of the total supply of this good in the marketplace
    /// </summary>
    [JsonPropertyName("supply")]
    public MarketSupply Supply { get; set; }

    /// <summary>
    /// The price in which the good can be purchased from the market
    /// </summary>
    [JsonPropertyName("purchasePrice")]
    public int PurchasePrice { get; set; }

    /// <summary>
    /// The price in which the good can be sold to the market
    /// </summary>
    [JsonPropertyName("sellPrice")]
    public int SellPrice { get; set; }
}