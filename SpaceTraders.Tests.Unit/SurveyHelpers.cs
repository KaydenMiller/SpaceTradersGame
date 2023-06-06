using SpaceTraders.Core;

namespace SpaceTraders.Tests.Unit;

public static class SurveyHelpers
{
    public static Survey GenerateSurvey(params OreDeposits[] types)
    {
        var survey = new Survey()
        {
            Symbol = Guid.NewGuid().ToString("D"),
            Expiration = DateTime.UtcNow + TimeSpan.FromHours(1)
        };
        var deposits = types.Select(type => new Survey.OreDeposit()
        {
            Deposit = type
        });
        survey.OreDeposits = deposits;
        return survey;
    }
}

public static class MarketHelpers
{
    public static MarketTradeGood GenerateMarketTradeGood(OreDeposits id, int price)
    {
        return new MarketTradeGood()
        {
            Id = id.ToString(),
            SellPrice = price
        };
    }
}