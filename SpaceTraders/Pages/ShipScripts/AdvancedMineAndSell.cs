using System.Collections.Immutable;
using System.Security.Cryptography.X509Certificates;
using SpaceTraders.Core;
using SpaceTraders.Pages.Location;
using SpaceTraders.Pages.Ship;

namespace SpaceTraders.Pages.ShipScripts;

public class AdvancedMineAndSell : IScript
{
    private readonly ShipApiService _shipApiService;
    private readonly LocationApiService _locationApiService;
    private readonly ILogger<AdvancedMineAndSell> _logger;
    public string Name { get; } = nameof(AdvancedMineAndSell);
    public bool Running { get; private set; }

    public IEnumerable<MarketTradeGood> TradeGoods { get; set; } = Enumerable.Empty<MarketTradeGood>();
    public AdvancedMineAndSell(ShipApiService shipApiService,
        LocationApiService locationApiService,
        ILogger<AdvancedMineAndSell> logger) {
        _shipApiService = shipApiService;
        _locationApiService = locationApiService;
        _logger = logger;
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
                ship.NavigationInfo.WaypointSymbol))).TradeGoods;
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

        var surveyToUse = GetBestSurvey(goodsToKeep);
        if (surveyToUse is not null)
            _logger.LogInformation("{ScriptId}: {ShipId}: Using survey {Signature}; {Items}", nameof(AdvancedMineAndSell), ship.Id, surveyToUse.Signature, surveyToUse.OreDeposits.Select(x => x.Deposit));
        else 
            _logger.LogInformation("{ScriptId}: {ShipId}: No survey found", nameof(AdvancedMineAndSell), ship.Id);
        
        do
        {
            await Task.Delay(waitTime ?? TimeSpan.Zero);
            var extraction = await PerformAction(async () => await _shipApiService.ExtractOre(ship, surveyToUse ?? null));
            ship.Cargo = extraction.Cargo;
            waitTime = extraction.Cooldown.GetWaitTime();
            _logger.LogInformation("{ScriptId}: {ShipId}: Extracted {Quantity} of {Name}", nameof(AdvancedMineAndSell), extraction.Extraction.ShipSymbol, extraction.Extraction.Yield.Units, extraction.Extraction.Yield.Symbol);
            
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
        foreach (var cargoItem in ship.Cargo.InventoryItems)
        {
            var sellCargoResponse = await PerformAction(async () => await _shipApiService.SellCargo(ship, cargoItem.Id, cargoItem.Quantity));
            updatedCargo = sellCargoResponse.Cargo;
            _logger.LogInformation("{ScriptId}: {ShipId}: sold {Quantity} {Item}(s) for {TotalPrice}", nameof(AdvancedMineAndSell), ship.Id,
                sellCargoResponse.Transaction.Units, sellCargoResponse.Transaction.TradeSymbol, sellCargoResponse.Transaction.TotalPrice);
        }
        if (updatedCargo is not null) ship.Cargo = updatedCargo;

        Running = false;
    }

    private Survey? GetBestSurvey(ImmutableArray<string> goodsToMine)
    {
        var surveys = _shipApiService.Surveys.ToImmutableArray();
        var surveysThatContainGoodDeposits = surveys
           .Where(s => s.OreDeposits
               .Select(od => od.Deposit)
               .Any(od => goodsToMine.Contains(od.ToString())));
        var bestSurvey = surveysThatContainGoodDeposits
           .Select(s =>
            {
                var countOfGoodOres = s.OreDeposits.Count(x => goodsToMine.Contains(x.Deposit.ToString()));
                return new
                {
                    Survey = s,
                    CountOfGoodOres = countOfGoodOres
                };
            }).MaxBy(x => x.CountOfGoodOres)?.Survey;
        return bestSurvey;
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