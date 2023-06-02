using SpaceTraders.Core;

namespace SpaceTraders.Algorithms;

public record SurveyWeight(int Weight, Survey Survey);


public class SurveyMarketViabilityWeightCalculation
{
    private record DepositInfo(OreDeposits Type, int Count);

    private record ItemWeight(OreDeposits Type, int Weight);

    private record ItemWithMarketData(OreDeposits Type, DepositInfo DepositInfo, MarketTradeGood AssociatedTradeGood);

    public SurveyWeight Calculate(Survey survey, IEnumerable<MarketTradeGood> tradeGoods)
    {
        var depositGroups = survey.OreDeposits.GroupBy(x => x.Deposit);
        var depositData = depositGroups.Select(s => new DepositInfo(s.Key, s.Count()));
        var mergedData = depositData.Join(tradeGoods, depositInfo => depositInfo.Type.ToString(), mtg => mtg.Id,
            (data, good) => new ItemWithMarketData(data.Type, data, good));
        var totalDeposits = survey.OreDeposits.Count();
        var weightedItems = mergedData.Select(item =>
        {
            var itemWeight = item.AssociatedTradeGood.SellPrice * item.DepositInfo.Count;
            return new ItemWeight(item.Type, itemWeight);
        });
        var surveyWeight = weightedItems.Sum(item => item.Weight) / totalDeposits;
        return new SurveyWeight(surveyWeight, survey);
    }
    
    public IEnumerable<SurveyWeight> Calculate(IEnumerable<Survey> surveys, IEnumerable<MarketTradeGood> tradeGoods)
    {
        var validSurveys = surveys
           .Where(x => DateTime.UtcNow < x.Expiration);

        var weightedSurveys = validSurveys.Select(s => Calculate(s, tradeGoods));

        return weightedSurveys;
    }

}