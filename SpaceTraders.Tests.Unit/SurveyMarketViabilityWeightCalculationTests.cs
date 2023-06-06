using FluentAssertions;
using SpaceTraders.Algorithms;
using SpaceTraders.Core;
using SpaceTraders.Pages.ShipScripts;

namespace SpaceTraders.Tests.Unit;

public class SurveyMarketViabilityWeightCalculationTests
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var surveys = new Survey[]
        {
            SurveyHelpers.GenerateSurvey(OreDeposits.ICE_WATER, OreDeposits.ICE_WATER, OreDeposits.ICE_WATER),
            SurveyHelpers.GenerateSurvey(OreDeposits.GOLD_ORE),
            SurveyHelpers.GenerateSurvey(OreDeposits.ICE_WATER, OreDeposits.GOLD_ORE),
            SurveyHelpers.GenerateSurvey(OreDeposits.ICE_WATER, OreDeposits.GOLD_ORE, OreDeposits.GOLD_ORE),
            SurveyHelpers.GenerateSurvey(OreDeposits.DIAMONDS, OreDeposits.SILICON_CRYSTALS, OreDeposits.SILICON_CRYSTALS, OreDeposits.SILICON_CRYSTALS),
        };
        var goods = new MarketTradeGood[]
        {
            MarketHelpers.GenerateMarketTradeGood(OreDeposits.ICE_WATER, 1),
            MarketHelpers.GenerateMarketTradeGood(OreDeposits.GOLD_ORE, 4),
            MarketHelpers.GenerateMarketTradeGood(OreDeposits.SILICON_CRYSTALS, 1),
            MarketHelpers.GenerateMarketTradeGood(OreDeposits.DIAMONDS, 1000),
        };
        var sut = new SurveyMarketViabilityWeightCalculation();

        // Act
        var weightedSurveys = sut.Calculate(surveys, goods).ToList();

        // Assert
        weightedSurveys.Should().Contain(new[]
        {
            new SurveyWeight(1, surveys[0]), // (1 + 1 + 1) * 3 / 3 = 1
            new SurveyWeight(4, surveys[1]), // (4) * 1 / 1 = 4
            new SurveyWeight(2, surveys[2]), // ((1 * 1) + (4 * 1)) = 5 / 2 = 2.5 ~= 2
            new SurveyWeight(3, surveys[3]), // ((1 * 1) + (4 * 2)) = 9 / 3 = 3
            new SurveyWeight(250, surveys[4]), // ((1000 * 1) + (1 * 3)) = 1003 / 4 = 250.75 ~= 250
        });
    }
}