using System.Collections.Immutable;
using Flurl.Http;
using MediatR;
using MudBlazor;
using Polly;
using SpaceTraders.Algorithms;
using SpaceTraders.Core;
using SpaceTraders.HttpPolicies;
using SpaceTraders.Pages.Location;
using SpaceTraders.Pages.Notification;
using SpaceTraders.Pages.Ship;

namespace SpaceTraders.Pages.ShipScripts;

public class AdvancedMineAndSell : IScript
{
    private readonly ShipApiService _shipApiService;
    private readonly LocationApiService _locationApiService;
    private readonly ILogger<AdvancedMineAndSell> _logger;
    private readonly IMediator _mediator;

    public string Name { get; } = nameof(AdvancedMineAndSell);
    public bool Running { get; private set; }

    public IEnumerable<MarketTradeGood> TradeGoods { get; set; } = Enumerable.Empty<MarketTradeGood>();
    public AdvancedMineAndSell(ShipApiService shipApiService,
        LocationApiService locationApiService,
        ILogger<AdvancedMineAndSell> logger,
        IMediator mediator) {
        _shipApiService = shipApiService;
        _locationApiService = locationApiService;
        _logger = logger;
        _mediator = mediator;
    }
    
    public async Task Run(Core.Ship ship)
    {
        Running = true;

        // Update the ship
        ship = await PerformAction(async () => await _shipApiService.GetShipDetail(ship.Id));
        
        // if the ship is undocked
        if (ship.NavigationInfo.Status == ShipStatus.IN_ORBIT)
        {
            _ = await PerformAction(async () => await _shipApiService.DockCurrent(ship));
            ship.NavigationInfo = (await PerformAction(async () => await _shipApiService.GetShipDetail(ship.Id))).NavigationInfo;
            _logger.LogInformation("{ScriptId}: {ShipId}: Docked Ship at {Waypoint}", nameof(AdvancedMineAndSell), ship.Id, ship.NavigationInfo.WaypointSymbol.ToString());
        }

        // Get local trade goods
        TradeGoods = (await PerformAction(async () => await _locationApiService.GetMarketPlace(ship.NavigationInfo.WaypointSymbol,
                ship.NavigationInfo.WaypointSymbol))).TradeGoods.ToList();
        _logger.LogInformation("{ScriptId}: {ShipId}: getting marketplace data", nameof(AdvancedMineAndSell), ship.Id);
        
        // What is the best good to be mining
        var goodsToKeep = TradeGoods
           .OrderByDescending(x => x.SellPrice)
           .Where(x => x.SellPrice > 3)
           .Select(x => x.Id)
           .ToImmutableArray();
        _logger.LogInformation("{ScriptId}: {ShipId}: determined valid trades to be {ValidTrades}", nameof(AdvancedMineAndSell), ship.Id, goodsToKeep);

        // Undock ship so we can jettison cargo
        _ = await PerformAction(async () => await _shipApiService.OrbitCurrent(ship));
        _logger.LogInformation("{ScriptId}: {ShipId}: Undocked ship at {Waypoint}", nameof(AdvancedMineAndSell), ship.Id, ship.NavigationInfo.WaypointSymbol);

        // Jettison and Update Cargo Manifest
        await JettisonAndUpdateCargo(ship, goodsToKeep);

        // Is the ship on cooldown?
        var waitTime = (await PerformAction(async () => await _shipApiService.GetShipCooldown(ship)))
            ?.GetWaitTime();

        var surveyAlgorithm = new SurveyMarketViabilityWeightCalculation();
        
        do
        {
            var weightedSurveys = surveyAlgorithm.Calculate(_shipApiService.Surveys, TradeGoods).ToList();
            _logger.LogInformation("{ScriptId}: {ShipId}: Weighted Surveys {WeightedSurveys}", nameof(AdvancedMineAndSell), ship.Id, weightedSurveys);
            _logger.LogInformation("{ScriptId}: {ShipId}: Market Prices {@MarketPrices}", nameof(AdvancedMineAndSell), ship.Id, TradeGoods.Select(x => new { Id = x.Id, SellFor = x.SellPrice }));
            var surveyToUse = weightedSurveys.MaxBy(s => s.Weight)?.Survey;
            if (surveyToUse is not null)
            {
                _logger.LogInformation("{ScriptId}: {ShipId}: Using survey {Signature}; {Items}",
                    nameof(AdvancedMineAndSell), ship.Id, surveyToUse.Signature,
                    surveyToUse.OreDeposits.Select(x => x.Deposit));
                _logger.LogInformation("{ScriptId}: {ShipId}: {SurveyId}: {SurveyExpiration}",
                    nameof(AdvancedMineAndSell), ship.Id, surveyToUse.Signature, surveyToUse.Expiration.ToLocalTime().ToString("g"));
            }
            else 
                _logger.LogInformation("{ScriptId}: {ShipId}: No survey found", nameof(AdvancedMineAndSell), ship.Id);
            
            await Task.Delay(waitTime ?? TimeSpan.Zero);
            try
            {
                var extraction = await Policy
                   .WrapAsync(
                        SpaceTradersHttpPolicyHelpers.SpaceTradersPolicyAsync(),
                        SpaceTradersHttpPolicyHelpers.ShipCooldownPolicy() 
                        // Policy
                        //    .Handle<SpaceTradersApiException>()
                        //    .FallbackAsync<ExtractionResponse>(async (_) =>
                        //     {
                        //         _logger.LogInformation("{ScriptId}: {ShipId}: Could not extract using survey because it was expired", nameof(AdvancedMineAndSell), ship.Id);
                        //         return await PerformAction(async () => await _shipApiService.ExtractOre(ship));
                        //     })
                    )
                   .ExecuteAsync(async () =>
                    {
                        return await PerformAction(async () => await _shipApiService.ExtractOre(ship, surveyToUse ?? null)); 
                    });

                ship.Cargo = extraction.Cargo;
                waitTime = extraction.Cooldown.GetWaitTime();
                _logger.LogInformation("{ScriptId}: {ShipId}: Extracted {Quantity} of {Name}", nameof(AdvancedMineAndSell), extraction.Extraction.ShipSymbol, extraction.Extraction.Yield.Units, extraction.Extraction.Yield.Symbol);
                _mediator.Publish(new SnackBarNotification(ship.Id,
                    $"Extracted {extraction.Extraction.Yield.Units} of {extraction.Extraction.Yield.Symbol}",
                    Severity.Normal));
            }
            catch (FlurlHttpException fhe)
            {
                var data = await fhe.GetResponseStringAsync();
                _logger.LogError("Flurl received an error, {StatusCode}; {Data}", fhe.StatusCode, data);
            }

            // Clean out bad cargo to save on request time
            await JettisonAndUpdateCargo(ship, goodsToKeep);
        } while (ship.Cargo.TotalUnits < ship.Cargo.Capacity);
        
        // Cargo is full dock at station
        _ = await PerformAction(async () => await _shipApiService.DockCurrent(ship));
        ship.NavigationInfo = (await PerformAction(async () => await _shipApiService.GetShipDetail(ship.Id)))
           .NavigationInfo;
        _logger.LogInformation("{ScriptId}: {ShipId}: Docked Ship at {Waypoint}", nameof(AdvancedMineAndSell), ship.Id, ship.NavigationInfo.WaypointSymbol);
       // Sell Cargo
        Cargo? updatedCargo = null;
        var totalGained = 0;
        foreach (var cargoItem in ship.Cargo.InventoryItems)
        {
            var sellCargoResponse = await PerformAction(async () => await _shipApiService.SellCargo(ship, cargoItem.Id, cargoItem.Quantity));
            updatedCargo = sellCargoResponse.Cargo;
            totalGained += sellCargoResponse.Transaction.TotalPrice;
            _logger.LogInformation("{ScriptId}: {ShipId}: sold {Quantity} {Item}(s) for {TotalPrice}", nameof(AdvancedMineAndSell), ship.Id,
                sellCargoResponse.Transaction.Units, sellCargoResponse.Transaction.TradeSymbol, sellCargoResponse.Transaction.TotalPrice);
        }
        _mediator.Publish(new SnackBarNotification(ship.Id, $"Sold its inventory for: {totalGained}", Severity.Info));
        if (updatedCargo is not null) ship.Cargo = updatedCargo;

        Running = false;
    }

    private async Task JettisonAndUpdateCargo(Core.Ship ship, ImmutableArray<string> goodsToKeep)
    {
        Cargo? updatedCargo = null;
        foreach (var item in ship.Cargo.InventoryItems)
        {
            if (!goodsToKeep.Contains(item.Id))
            {
                updatedCargo =
                    await PerformAction(async () => await _shipApiService.JettisonCargo(ship, item.Id, item.Quantity));
                _logger.LogInformation("{ScriptId}: {ShipId}: Jettisoned {Quantity} of {Name}", nameof(AdvancedMineAndSell),
                    ship.Id, item.Quantity, item.Id);
            }
        }

        ship.Cargo = await PerformAction(async () => await _shipApiService.GetShipCargo(ship));
    }

    private async Task<TResult> PerformAction<TResult>(Func<Task<TResult>> callback, int timeoutMilliseconds = 5000)
    {
        var result = await callback.Invoke();
        await Task.Delay(timeoutMilliseconds / 1000);
        return result;
    }
}